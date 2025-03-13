using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;    //Disable "Cancel" button initially
        }

        //Used for managing cancellation
        private CancellationTokenSource _cts;

        private async void button1_Click(object sender, EventArgs e)
        {
            //Initialize a new CancellationTokenSource
            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            //Disable the button and reset the label
            button1.Enabled = false;
            button2.Enabled = true;     // Enable "Cancel" button
            progressBar1.Value = 0;     //Reset ProgressBar
            label1.Text = "Progress: 0%";

            try
            {
                //Perform a long-running calculation task with real-time progress updates
                int result = await Task.Run(async () =>
                {
                    int sum = 0;

                    for (int i = 1; i <= 100; i++) //Loop from 1 to 100 (scale for 100%)
                    {
                        //Check for cancellation
                        if (token.IsCancellationRequested)
                            return i;   //Return progress value when canceled

                        //Simulate work (delay for computation)
                        await Task.Delay(50);

                        //Update the percentage progress and partial sum on the UI thread
                        this.Invoke((Action)(() =>
                        {
                            progressBar1.Value = i;     //Update ProgressBar
                            label1.Text = $"Progress {i}%\n" +
                                            $"(Partial Sum: {sum})";
                        }));

                        sum += i;
                    }

                    return sum; //Return the final sum

                }, token);      //Pass the CancellationToken

                //If the task completes, show the final result
                if (result > 100)
                {
                    label1.Text = $"Calculation Complete!\n" +
                                    $"Result: {result}";
                }
                else
                {
                    label1.Text = $"Task Canceled at {result}%!";
                }
            }
            finally
            {
                //Reset UI elements
                button1.Enabled = true;     //Re-enable "Start" button
                button2.Enabled = false;    //Disable "Cancel" button

                //Dispose of the CancellationTokenSource
                _cts.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Trigger cancellation if CancellationTokenSource exists
            _cts?.Cancel();
        }
    }
}
