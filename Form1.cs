using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Form1 : Form
    {
        bool turn = true; // true är x turn och false är O tur
        int turn_count = 0;
        bool against_computer = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void about_button_click(object sender, EventArgs e)
        {
            MessageBox.Show("Sebbe och Ariens TicTacToe", "TicTacToe About");
        }


        private void exit_button_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newgame_button_click(object sender, EventArgs e)
        {
            turn=true;
            turn_count=0;

            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled=true;
                    b.Text="";
                }
                catch { }
            }
        }

        private void button_click(object sender, EventArgs e)
        {

            Button b = (Button)sender;

            if (turn)
                b.Text = "X";
            else
                b.Text="O";

            turn = !turn;

            b.Enabled = false;
            turn_count++;

            check_for_winner();

            

            if ((!turn) && (against_computer))
            {
                computer_make_move();
            }
        }

        private void computer_make_move()
        {
            Thread.Sleep(300);

            Button move = null;

            move = look_for_win_or_block("O"); 
            if (move == null)
            {
                move = look_for_win_or_block("X");
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }
                }
            }

            move.PerformClick();
        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block:  " + mark);
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }



        private void check_for_winner()
        {
            bool there_is_a_winner = false;

            //bara horisontellt
            if ((A1.Text == A2.Text)&&(A2.Text==A3.Text)&&(!A1.Enabled))
                there_is_a_winner=true;
            else if ((B1.Text == B2.Text)&&(B2.Text==B3.Text)&&(!B1.Enabled))
                there_is_a_winner=true;
            else if ((C1.Text == C2.Text)&&(C2.Text==C3.Text)&&(!C1.Enabled))
                there_is_a_winner=true;

            //bara vertikallt
            else if ((A1.Text == B1.Text)&&(B1.Text==C1.Text)&&(!A1.Enabled))
                there_is_a_winner=true;
            else if ((A2.Text == B2.Text)&&(B2.Text==C2.Text)&&(!A2.Enabled))
                there_is_a_winner=true;
            else if ((A3.Text == B3.Text)&&(B3.Text==C3.Text)&&(!A3.Enabled))
                there_is_a_winner=true;

            //snett
            else if ((A1.Text == B2.Text)&&(B2.Text==C3.Text)&&(!A1.Enabled))
                there_is_a_winner=true;
            else if ((C1.Text == B2.Text)&&(B2.Text==A3.Text)&&(!C1.Enabled))
                there_is_a_winner=true;

            if (there_is_a_winner)
            {

                string winner = "";
                if (turn)
                {
                    winner="O";
                    O_win_count.Text = (Int32.Parse(O_win_count.Text) +1).ToString();
                    DialogResult WinnerMessageO = MessageBox.Show(winner +" Vinner!" + "\n" + "Vill du spela igen spel?", "Vinst", MessageBoxButtons.YesNo);
                    if (WinnerMessageO==DialogResult.Yes)
                    {
                        new_game();
                    }
                    if (WinnerMessageO==DialogResult.No)
                    {
                        exit_game();
                    }
                }
                else
                {
                    winner="X";
                    X_win_count.Text = (Int32.Parse(X_win_count.Text) +1).ToString();

                    DialogResult WinnerMessageX = MessageBox.Show(winner +" Vinner!"+"\n" + "Vill du spela igen spel?", "Vinst", MessageBoxButtons.YesNo);
                    if (WinnerMessageX==DialogResult.Yes)
                    {
                        new_game();
                    }
                    if (WinnerMessageX==DialogResult.No)
                    {
                        exit_game();
                    }

                }
            }
            else
            {
                if (turn_count==9)
                {
                    DialogResult WinnerMessageDraw = MessageBox.Show("Det blev lika " + "\n" + "Vill du spela igen spel?", "Ingen vann!", MessageBoxButtons.YesNo);
                    if (WinnerMessageDraw==DialogResult.Yes)
                    {
                        new_game();
                    }
                    if (WinnerMessageDraw==DialogResult.No)
                    {
                        exit_game();
                    }
                    Draw_win_count.Text = (Int32.Parse(Draw_win_count.Text) +1).ToString();
                }
            }
        }


        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";

                else
                    b.Text ="O";
            }
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text ="";
            }
        }

        private void new_game()
        {
            turn=true;
            turn_count=0;

            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Enabled=true;
                    b.Text="";
                }
                catch { }
            }

        }
        private void exit_game()
        {
            Application.Exit();
        }

        private void playervsai_button_click(object sender, EventArgs e)
        {
            against_computer=true;
        }

        private void playervsplayer_button_click(object sender, EventArgs e)
        {
            against_computer=false;
        }

        private void help_button_click(object sender, EventArgs e)
        {

        }
        private void options_button_click(object sender, EventArgs e)
        {

        }
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

} 
