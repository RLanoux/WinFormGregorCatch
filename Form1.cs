using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormGregorCatch
{
    public partial class Form1 : Form
    {
        // Set global constants
        const String APP_VERSION = " v1.0";
        const Int32 XLIMIT = 8;
        const Int32 YLIMIT = 8;

        // Establish new global variables
        Int32 iNewX = 0;
        Int32 iOldX = 0;
        Int32 iNewY = 0;
        Int32 iOldY = 0;
        Int32 iTries = 0;
        Int32[,] iKitchenFloor = new Int32[XLIMIT, YLIMIT];
        Random rnd = new Random();
        Boolean bStart = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void DoWork(Int32 x, Int32 y)
        {
            if (bStart)
            {
                // Increment iTries and display it in the textbox
                iTries++;
                txtTries.Text = iTries.ToString();

                // if x = iNewX and y = iNewY
                if ((x == iNewX) && (y == iNewY))
                {
                    //      then player captured Gregor
                    //      Display a congratulations victory message
                    MessageBox.Show("Congratulations!! You have caught the roach that has been terrorizing your kitchen.",
                    "Catching Gregor! " + APP_VERSION,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                    //      Sweep the kitchen (set array back to zeroes)
                    SweepKitchen();
                    // set iTries to 0 and update the textbox 
                    iTries = 0;
                    txtTries.Text = iTries.ToString();
                    //      Enable the Start button
                    btnStart.Enabled = true;
                    //      Enable the Exit button
                    btnExit.Enabled = true;
                    //Exit boolean
                    bStart = false;
                }

                // else
                // randomly select new tile coordinates that
                // are adjacent to but not the same as current tile
                // these will be Gregor's new position.
                else
                {
                    iOldX = iNewX;
                    iOldY = iNewY;
                    if (iNewX == XLIMIT - 1)
                    {
                        iNewX = rnd.Next(iOldX - 1, XLIMIT);
                    }
                    else if (iNewX == 0)
                    {
                        iNewX = rnd.Next(iOldX, iOldX + 2);
                    }
                    else
                    {
                        iNewX = rnd.Next(iOldX - 1, iOldX + 2);
                    }
                    if (iNewY == YLIMIT - 1)
                    {
                        iNewY = rnd.Next(iOldY - 1, YLIMIT);
                    }
                    else if (iNewY == 0)
                    {
                        iNewY = rnd.Next(iOldY, iOldY + 2);
                    }
                    else
                    {
                        iNewY = rnd.Next(iOldY - 1, iOldY + 2);
                    }

                    //Rule out selection of the same tile
                    while ((iOldX == iNewX) && (iOldY == iNewY))
                    {
                        if (iNewX == XLIMIT - 1)
                        {
                            iNewX = rnd.Next(iOldX - 1, XLIMIT);
                        }
                        else if (iNewX == 0)
                        {
                            iNewX = rnd.Next(iOldX, iOldX + 2);
                        }
                        else
                        {
                            iNewX = rnd.Next(iOldX - 1, iOldX + 2);
                        }

                        if (iNewY == YLIMIT - 1)
                        {
                            iNewY = rnd.Next(iOldY - 1, YLIMIT);
                        }
                        else if (iNewY == 0)
                        {
                            iNewY = rnd.Next(iOldY, iOldY + 1);
                        }
                        else
                        {
                            iNewY = rnd.Next(iOldY - 1, iOldY + 2);
                        }
                    }

                    // if Gregor's position matches player,
                    if ((x == iNewX) && (y == iNewY))
                    {
                        // Tell user he lost
                        MessageBox.Show("Oh goodness!! Gregor has crawled up your leg and is now scurrying around on your clothes!",
                        "Catching Gregor! " + APP_VERSION,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                        //      Sweep the kitchen (set array back to zeroes)
                        SweepKitchen();
                        // set iTries to 0 and update the textbox 
                        iTries = 0;
                        txtTries.Text = iTries.ToString();
                        //      Enable the Start button
                        btnStart.Enabled = true;
                        //      Enable the Exit button
                        btnExit.Enabled = true;
                        //Exit boolean
                        bStart = false;
                    }
                }
            }
        }

        /************************************************************************
         * Do not modify any code below this point! You will break the program! *
         * You may click on the + signs along the left to examine each of the   *
         * methods to see what they do, but don't change them. You will use     *
         * them by calling them from the main program.                          *
         ************************************************************************/

        /// <summary>
        /// SetGregor() is a method that "drops" Gregor onto a random tile.
        /// </summary>
        private void SetGregor()
        {
            // randomly select a tile
            iNewX = rnd.Next(0, XLIMIT);
            iNewY = rnd.Next(0, YLIMIT);
        }

        /// <summary>
        /// SweepKitchen() is a method that resets all tiles to zero
        /// </summary>
        private void SweepKitchen()
        {
            // Set the values to zero
            for (Int32 j = 0; j < YLIMIT; j++)
            {
                for (Int32 i = 0; i < XLIMIT; i++)
                {
                    iKitchenFloor[j, i] = 0;
                }
            }
        }

        /// <summary>
        /// This initializes the title bar and places text in the instruction label.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Catching Gregor!" + APP_VERSION;
            lblWhere.Text = "Where is Gregor??\n\nClick on\nkitchen\nfloor tiles\nuntil you\nfind him!"
                + "\n\nClick Start to\nbegin your hunt!";
        }

        /************************************************************************
         * Each of the following methods are the event handlers for each of the *
         * tiles. Each one calls the DoWork() method, sending its own X and Y   *
         * coordinates, so the DoWork() method can compare your click to Gregor.*
         ************************************************************************/
        private void lblTile00_Click(object sender, EventArgs e)
        {
            DoWork(0, 0);
        }

        private void lblTile01_Click(object sender, EventArgs e)
        {
            DoWork(0, 1);
        }

        private void lblTile02_Click(object sender, EventArgs e)
        {
            DoWork(0, 2);
        }

        private void lblTile03_Click(object sender, EventArgs e)
        {
            DoWork(0, 3);
        }

        private void lblTile04_Click(object sender, EventArgs e)
        {
            DoWork(0, 4);
        }

        private void lblTile05_Click(object sender, EventArgs e)
        {
            DoWork(0, 5);
        }

        private void lblTile06_Click(object sender, EventArgs e)
        {
            DoWork(0, 6);
        }

        private void lblTile07_Click(object sender, EventArgs e)
        {
            DoWork(0, 7);
        }

        private void lblTile17_Click(object sender, EventArgs e)
        {
            DoWork(1, 7);
        }

        private void lblTile16_Click(object sender, EventArgs e)
        {
            DoWork(1, 6);
        }

        private void lblTile15_Click(object sender, EventArgs e)
        {
            DoWork(1, 5);
        }

        private void lblTile14_Click(object sender, EventArgs e)
        {
            DoWork(1, 4);
        }

        private void lblTile13_Click(object sender, EventArgs e)
        {
            DoWork(1, 3);
        }

        private void lblTile12_Click(object sender, EventArgs e)
        {
            DoWork(1, 2);
        }

        private void lblTile11_Click(object sender, EventArgs e)
        {
            DoWork(1, 1);
        }

        private void lblTile10_Click(object sender, EventArgs e)
        {
            DoWork(1, 0);
        }

        private void lblTile20_Click(object sender, EventArgs e)
        {
            DoWork(2, 0);
        }

        private void lblTile21_Click(object sender, EventArgs e)
        {
            DoWork(2, 1);
        }

        private void lblTile22_Click(object sender, EventArgs e)
        {
            DoWork(2, 2);
        }

        private void lblTile23_Click(object sender, EventArgs e)
        {
            DoWork(2, 3);
        }

        private void lblTile24_Click(object sender, EventArgs e)
        {
            DoWork(2, 4);
        }

        private void lblTile25_Click(object sender, EventArgs e)
        {
            DoWork(2, 5);
        }

        private void lblTile26_Click(object sender, EventArgs e)
        {
            DoWork(2, 6);
        }

        private void lblTile27_Click(object sender, EventArgs e)
        {
            DoWork(2, 7);
        }

        private void lblTile37_Click(object sender, EventArgs e)
        {
            DoWork(3, 7);
        }

        private void lblTile36_Click(object sender, EventArgs e)
        {
            DoWork(3, 6);
        }

        private void lblTile35_Click(object sender, EventArgs e)
        {
            DoWork(3, 5);
        }

        private void lblTile34_Click(object sender, EventArgs e)
        {
            DoWork(3, 4);
        }

        private void lblTile33_Click(object sender, EventArgs e)
        {
            DoWork(3, 3);
        }

        private void lblTile32_Click(object sender, EventArgs e)
        {
            DoWork(3, 2);
        }

        private void lblTile31_Click(object sender, EventArgs e)
        {
            DoWork(3, 1);
        }

        private void lblTile30_Click(object sender, EventArgs e)
        {
            DoWork(3, 0);
        }

        private void lblTile40_Click(object sender, EventArgs e)
        {
            DoWork(4, 0);
        }

        private void lblTile41_Click(object sender, EventArgs e)
        {
            DoWork(4, 1);
        }

        private void lblTile42_Click(object sender, EventArgs e)
        {
            DoWork(4, 2);
        }

        private void lblTile43_Click(object sender, EventArgs e)
        {
            DoWork(4, 3);
        }

        private void lblTile44_Click(object sender, EventArgs e)
        {
            DoWork(4, 4);
        }

        private void lblTile45_Click(object sender, EventArgs e)
        {
            DoWork(4, 5);
        }

        private void lblTile46_Click(object sender, EventArgs e)
        {
            DoWork(4, 6);
        }

        private void lblTile47_Click(object sender, EventArgs e)
        {
            DoWork(4, 7);
        }

        private void lblTile57_Click(object sender, EventArgs e)
        {
            DoWork(5, 7);
        }

        private void lblTile56_Click(object sender, EventArgs e)
        {
            DoWork(5, 6);
        }

        private void lblTile55_Click(object sender, EventArgs e)
        {
            DoWork(5, 5);
        }

        private void lblTile54_Click(object sender, EventArgs e)
        {
            DoWork(5, 4);
        }

        private void lblTile53_Click(object sender, EventArgs e)
        {
            DoWork(5, 3);
        }

        private void lblTile52_Click(object sender, EventArgs e)
        {
            DoWork(5, 2);
        }

        private void lblTile51_Click(object sender, EventArgs e)
        {
            DoWork(5, 1);
        }

        private void lblTile50_Click(object sender, EventArgs e)
        {
            DoWork(5, 0);
        }

        private void lblTile60_Click(object sender, EventArgs e)
        {
            DoWork(6, 0);
        }

        private void lblTile61_Click(object sender, EventArgs e)
        {
            DoWork(6, 1);
        }

        private void lblTile62_Click(object sender, EventArgs e)
        {
            DoWork(6, 2);
        }

        private void lblTile63_Click(object sender, EventArgs e)
        {
            DoWork(6, 3);
        }

        private void lblTile64_Click(object sender, EventArgs e)
        {
            DoWork(6, 4);
        }

        private void lblTile65_Click(object sender, EventArgs e)
        {
            DoWork(6, 5);
        }

        private void lblTile66_Click(object sender, EventArgs e)
        {
            DoWork(6, 6);
        }

        private void lblTile67_Click(object sender, EventArgs e)
        {
            DoWork(6, 7);
        }

        private void lblTile77_Click(object sender, EventArgs e)
        {
            DoWork(7, 7);
        }

        private void lblTile76_Click(object sender, EventArgs e)
        {
            DoWork(7, 6);
        }

        private void lblTile75_Click(object sender, EventArgs e)
        {
            DoWork(7, 5);
        }

        private void lblTile74_Click(object sender, EventArgs e)
        {
            DoWork(7, 4);
        }

        private void lblTile73_Click(object sender, EventArgs e)
        {
            DoWork(7, 3);
        }

        private void lblTile72_Click(object sender, EventArgs e)
        {
            DoWork(7, 2);
        }

        private void lblTile71_Click(object sender, EventArgs e)
        {
            DoWork(7, 1);
        }

        private void lblTile70_Click(object sender, EventArgs e)
        {
            DoWork(7, 0);
        }

        /// <summary>
        /// This is the event handler for the Start button. It disables the two buttons,
        /// "Sweeps the kitchen" and drops Gregor onto a randomly chosen tile.
        /// The SweepKitchen() method is unnecessary, but is in here so we can add a feature
        /// later on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            bStart = true;
            btnStart.Enabled = false;
            btnExit.Enabled = false;
            SweepKitchen();
            SetGregor();
        }

        /// <summary>
        /// Duh.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Gives the user the option of quitting the game midstream.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEscape_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Awwww. Are you scared of a little bitty cockroach?",
                "Catching Gregor" + APP_VERSION,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
            this.Close();
        }
    }
}
