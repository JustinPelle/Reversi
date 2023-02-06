using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi
{
    partial class LaunchForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Rows_textbox = new System.Windows.Forms.TextBox();
            this.Rows_label = new System.Windows.Forms.Label();
            this.Columns_label = new System.Windows.Forms.Label();
            this.Columns_textbox = new System.Windows.Forms.TextBox();
            this.players_Label = new System.Windows.Forms.Label();
            this.Players_textBox = new System.Windows.Forms.TextBox();
            this.CPUplayers_Label = new System.Windows.Forms.Label();
            this.CPUPlayers_textBox = new System.Windows.Forms.TextBox();
            this.NewGame_button = new System.Windows.Forms.Button();
            this.Reversi_label = new System.Windows.Forms.Label();
            this.Explanation_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Rows_textbox
            // 
            this.Rows_textbox.Location = new System.Drawing.Point(225, 169);
            this.Rows_textbox.Name = "Rows_textbox";
            this.Rows_textbox.Size = new System.Drawing.Size(41, 20);
            this.Rows_textbox.TabIndex = 0;

            // 
            // Rows_label
            // 
            this.Rows_label.AutoSize = true;
            this.Rows_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rows_label.Location = new System.Drawing.Point(117, 168);
            this.Rows_label.Name = "Rows_label";
            this.Rows_label.Size = new System.Drawing.Size(56, 18);
            this.Rows_label.TabIndex = 1;
            this.Rows_label.Text = "Rows ";

            // 
            // Columns_label
            // 
            this.Columns_label.AutoSize = true;
            this.Columns_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Columns_label.Location = new System.Drawing.Point(117, 195);
            this.Columns_label.Name = "Columns_label";
            this.Columns_label.Size = new System.Drawing.Size(75, 18);
            this.Columns_label.TabIndex = 2;
            this.Columns_label.Text = "Columns";
            // 
            // columns_textbox
            // 
            this.Columns_textbox.Location = new System.Drawing.Point(225, 195);
            this.Columns_textbox.Name = "Columns_textbox";
            this.Columns_textbox.Size = new System.Drawing.Size(41, 20);
            this.Columns_textbox.TabIndex = 3;
            // 
            // players_Label
            // 
            this.players_Label.AutoSize = true;
            this.players_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.players_Label.Location = new System.Drawing.Point(117, 111);
            this.players_Label.Name = "players_Label";
            this.players_Label.Size = new System.Drawing.Size(69, 18);
            this.players_Label.TabIndex = 4;
            this.players_Label.Text = "Players ";
            // 
            // Players_textBox
            // 
            this.Players_textBox.Location = new System.Drawing.Point(225, 109);
            this.Players_textBox.Name = "Players_textBox";
            this.Players_textBox.Size = new System.Drawing.Size(41, 20);
            this.Players_textBox.TabIndex = 5;

            // CPUplayers_Label
            // 
            this.CPUplayers_Label.AutoSize = true;
            this.CPUplayers_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CPUplayers_Label.Location = new System.Drawing.Point(117, 132);
            this.CPUplayers_Label.Name = "players_Label";
            this.CPUplayers_Label.Size = new System.Drawing.Size(69, 18);
            this.CPUplayers_Label.TabIndex = 4;
            this.CPUplayers_Label.Text = "CPU players ";
            // 
            // CPUPlayers_textBox
            // 
            this.CPUPlayers_textBox.Location = new System.Drawing.Point(225, 132);
            this.CPUPlayers_textBox.Name = "Players_textBox";
            this.CPUPlayers_textBox.Size = new System.Drawing.Size(41, 20);
            this.CPUPlayers_textBox.TabIndex = 5;
            // 
            // NewGame_button
            // 
            this.NewGame_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewGame_button.Location = new System.Drawing.Point(134, 223);
            this.NewGame_button.Name = "NewGame_button";
            this.NewGame_button.Size = new System.Drawing.Size(99, 35);
            this.NewGame_button.TabIndex = 6;
            this.NewGame_button.Text = "New game";
            this.NewGame_button.UseVisualStyleBackColor = true;
            // 
            // 
            this.Reversi_label.AutoSize = true;
            this.Reversi_label.Font = new System.Drawing.Font("Arial Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reversi_label.Location = new System.Drawing.Point(129, 23);
            this.Reversi_label.Name = "Reversi_label";
            this.Reversi_label.Size = new System.Drawing.Size(114, 30);
            this.Reversi_label.TabIndex = 7;
            this.Reversi_label.Text = "REVERSI";

            // 
            // Explanation_label
            // 
            this.Explanation_label.AutoSize = true;
            this.Explanation_label.Location = new System.Drawing.Point(67, 68);
            this.Explanation_label.Name = "Explanation_label";
            this.Explanation_label.Size = new System.Drawing.Size(275, 26);
            this.Explanation_label.TabIndex = 8;
            this.Explanation_label.Text = "Choose the amount of players (max. 4 total) and\nthe size of your board. Enjoy this game of reve" +
                                           "rsi! ";
            // 
            // Start_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 330);
            this.Controls.Add(this.Explanation_label);
            this.Controls.Add(this.Reversi_label);
            this.Controls.Add(this.NewGame_button);
            this.Controls.Add(this.Players_textBox);
            this.Controls.Add(this.players_Label);
            this.Controls.Add(this.CPUPlayers_textBox);
            this.Controls.Add(this.CPUplayers_Label);
            this.Controls.Add(this.Columns_textbox);
            this.Controls.Add(this.Columns_label);
            this.Controls.Add(this.Rows_label);
            this.Controls.Add(this.Rows_textbox);
            this.Name = "Start_Form";
            this.Text = "Reversi new game ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Rows_textbox;
        private System.Windows.Forms.Label Rows_label;
        private System.Windows.Forms.Label Columns_label;
        private System.Windows.Forms.TextBox Columns_textbox;
        private System.Windows.Forms.Label players_Label;
        private System.Windows.Forms.TextBox Players_textBox;
        private System.Windows.Forms.Label CPUplayers_Label;
        private System.Windows.Forms.TextBox CPUPlayers_textBox;
        private System.Windows.Forms.Button NewGame_button;
        private System.Windows.Forms.Label Reversi_label;
        private System.Windows.Forms.Label Explanation_label;
    }
}
