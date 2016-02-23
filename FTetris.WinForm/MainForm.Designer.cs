namespace FTetris.WinForm
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.scoreLabel = new System.Windows.Forms.Label();
            this.scoreText = new System.Windows.Forms.Label();
            this.polyominoBoardView = new FTetris.WinForm.PolyominoBoardView();
            this.gameBoardView = new FTetris.WinForm.GameBoardView();
            this.SuspendLayout();
            // 
            // scoreLabel
            // 
            this.scoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Black;
            this.scoreLabel.ForeColor = System.Drawing.Color.White;
            this.scoreLabel.Location = new System.Drawing.Point(240, 103);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(40, 12);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "Score: ";
            // 
            // scoreText
            // 
            this.scoreText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scoreText.AutoSize = true;
            this.scoreText.BackColor = System.Drawing.Color.Black;
            this.scoreText.ForeColor = System.Drawing.Color.White;
            this.scoreText.Location = new System.Drawing.Point(280, 103);
            this.scoreText.Name = "scoreText";
            this.scoreText.Size = new System.Drawing.Size(11, 12);
            this.scoreText.TabIndex = 4;
            this.scoreText.Text = "0";
            this.scoreText.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // polyominoBoardView
            // 
            this.polyominoBoardView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.polyominoBoardView.BackColor = System.Drawing.Color.Black;
            this.polyominoBoardView.DataContext = null;
            this.polyominoBoardView.Enabled = false;
            this.polyominoBoardView.ForeColor = System.Drawing.Color.White;
            this.polyominoBoardView.Location = new System.Drawing.Point(239, 10);
            this.polyominoBoardView.Name = "polyominoBoardView";
            this.polyominoBoardView.Size = new System.Drawing.Size(80, 77);
            this.polyominoBoardView.TabIndex = 2;
            this.polyominoBoardView.TabStop = false;
            // 
            // gameBoardView
            // 
            this.gameBoardView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameBoardView.BackColor = System.Drawing.Color.Black;
            this.gameBoardView.DataContext = null;
            this.gameBoardView.ForeColor = System.Drawing.Color.White;
            this.gameBoardView.Location = new System.Drawing.Point(9, 9);
            this.gameBoardView.Margin = new System.Windows.Forms.Padding(10);
            this.gameBoardView.Name = "gameBoardView";
            this.gameBoardView.Size = new System.Drawing.Size(216, 620);
            this.gameBoardView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 639);
            this.Controls.Add(this.scoreText);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.polyominoBoardView);
            this.Controls.Add(this.gameBoardView);
            this.Name = "MainForm";
            this.Text = "FTetris(Enter: Start ←: Left →: Right ↑: Turn Right ↓: Turn Left Space: Drop)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GameBoardView gameBoardView;
        private PolyominoBoardView polyominoBoardView;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label scoreText;
    }
}

