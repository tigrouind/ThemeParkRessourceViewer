
namespace test
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pictureBox1 = new System.Windows.Forms.PictureBoxWithInterpolationMode();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setGameLocationFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToPNGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.xToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.useBilinearFilteringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.showAnimationElementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loopSoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Magenta;
			this.pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(173, 106);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Visible = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.settingsToolStripMenuItem,
									this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(513, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.setGameLocationFolderToolStripMenuItem,
									this.exportToPNGToolStripMenuItem,
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// setGameLocationFolderToolStripMenuItem
			// 
			this.setGameLocationFolderToolStripMenuItem.Name = "setGameLocationFolderToolStripMenuItem";
			this.setGameLocationFolderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.setGameLocationFolderToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
			this.setGameLocationFolderToolStripMenuItem.Text = "Set game location folder...";
			this.setGameLocationFolderToolStripMenuItem.Click += new System.EventHandler(this.SetGameLocationFolderToolStripMenuItemClick);
			// 
			// exportToPNGToolStripMenuItem
			// 
			this.exportToPNGToolStripMenuItem.Name = "exportToPNGToolStripMenuItem";
			this.exportToPNGToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.exportToPNGToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
			this.exportToPNGToolStripMenuItem.Text = "Export to...";
			this.exportToPNGToolStripMenuItem.Click += new System.EventHandler(this.ExportToPNGToolStripMenuItemClick);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(252, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItemClick);
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.zoomToolStripMenuItem,
									this.useBilinearFilteringToolStripMenuItem,
									this.showAnimationElementsToolStripMenuItem,
									this.loopSoundToolStripMenuItem});
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.settingsToolStripMenuItem.Text = "Settings";
			// 
			// zoomToolStripMenuItem
			// 
			this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.xToolStripMenuItem,
									this.xToolStripMenuItem1,
									this.xToolStripMenuItem2,
									this.xToolStripMenuItem3});
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.zoomToolStripMenuItem.Text = "Zoom";
			this.zoomToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ZoomToolStripMenuItemDropDownItemClicked);
			// 
			// xToolStripMenuItem
			// 
			this.xToolStripMenuItem.CheckOnClick = true;
			this.xToolStripMenuItem.Name = "xToolStripMenuItem";
			this.xToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
			this.xToolStripMenuItem.Tag = "1";
			this.xToolStripMenuItem.Text = "1x";
			// 
			// xToolStripMenuItem1
			// 
			this.xToolStripMenuItem1.Checked = true;
			this.xToolStripMenuItem1.CheckOnClick = true;
			this.xToolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
			this.xToolStripMenuItem1.Name = "xToolStripMenuItem1";
			this.xToolStripMenuItem1.Size = new System.Drawing.Size(85, 22);
			this.xToolStripMenuItem1.Tag = "2";
			this.xToolStripMenuItem1.Text = "2x";
			// 
			// xToolStripMenuItem2
			// 
			this.xToolStripMenuItem2.CheckOnClick = true;
			this.xToolStripMenuItem2.Name = "xToolStripMenuItem2";
			this.xToolStripMenuItem2.Size = new System.Drawing.Size(85, 22);
			this.xToolStripMenuItem2.Tag = "3";
			this.xToolStripMenuItem2.Text = "3x";
			// 
			// xToolStripMenuItem3
			// 
			this.xToolStripMenuItem3.CheckOnClick = true;
			this.xToolStripMenuItem3.Name = "xToolStripMenuItem3";
			this.xToolStripMenuItem3.Size = new System.Drawing.Size(85, 22);
			this.xToolStripMenuItem3.Tag = "4";
			this.xToolStripMenuItem3.Text = "4x";
			// 
			// useBilinearFilteringToolStripMenuItem
			// 
			this.useBilinearFilteringToolStripMenuItem.CheckOnClick = true;
			this.useBilinearFilteringToolStripMenuItem.Name = "useBilinearFilteringToolStripMenuItem";
			this.useBilinearFilteringToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.useBilinearFilteringToolStripMenuItem.Text = "Use bilinear filtering";
			this.useBilinearFilteringToolStripMenuItem.Click += new System.EventHandler(this.UseBilinearFilteringToolStripMenuItemClick);
			// 
			// showAnimationElementsToolStripMenuItem
			// 
			this.showAnimationElementsToolStripMenuItem.CheckOnClick = true;
			this.showAnimationElementsToolStripMenuItem.Name = "showAnimationElementsToolStripMenuItem";
			this.showAnimationElementsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.showAnimationElementsToolStripMenuItem.Text = "Show animation sprites";
			// 
			// loopSoundToolStripMenuItem
			// 
			this.loopSoundToolStripMenuItem.CheckOnClick = true;
			this.loopSoundToolStripMenuItem.Name = "loopSoundToolStripMenuItem";
			this.loopSoundToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.loopSoundToolStripMenuItem.Text = "Loop sound";
			this.loopSoundToolStripMenuItem.Click += new System.EventHandler(this.LoopSoundToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(185, 264);
			this.treeView1.TabIndex = 3;
			this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeView1BeforeExpand);
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
			// 
			// timer1
			// 
			this.timer1.Interval = 150;
			this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
			this.splitContainer1.Size = new System.Drawing.Size(513, 266);
			this.splitContainer1.SplitterDistance = 187;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 4;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(513, 294);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Theme Park Resource Viewer";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem loopSoundToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem useBilinearFilteringToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem showAnimationElementsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setGameLocationFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToPNGToolStripMenuItem;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.PictureBoxWithInterpolationMode pictureBox1;
								
		
	}
}
