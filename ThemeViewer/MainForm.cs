
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace test
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{		
		private string gameFolder;
		private SpriteTab[] SpriteTabs;
		private byte[] SpriteData;
		private SpriteFrame[] SpriteFrames;
		private SpriteFrame FirstSpriteFrame;
		private SpriteFrame CurrentSpriteFrame;
		private SpriteElement[] SpriteElements;
		private int[] Palette;
		private string lastDatFileOpen;
		private string lastAniFileOpen;
		private int zoom = 2;
		private Bitmap bitmapAnimation;
		private int animationOffsetX;
		private int animationOffsetY;
		private byte[] SoundData;
		private SoundTab[] SoundTabs;
		private byte[] soundBytes;
		private bool loopSound;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();					
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public TreeNode GetNode(string key, string text)
		{			
			if(!treeView1.Nodes.ContainsKey(key))
				return treeView1.Nodes.Add(key, text);						
			else						
				return treeView1.Nodes.Find(key, false)[0];		
		}
	
		
		void LoadTreeView()
		{
			
			
			
			
			string[] allfiles = System.IO.Directory.GetFiles(gameFolder)
				.Select(x => System.IO.Path.GetFileName(x)).ToArray();
			
			foreach(string file in allfiles			       
			        .OrderBy(x => x))
			{				
				string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file);
				if(file.Substring(0, 3) != "MUS" && file.Substring(0, 3) != "SND")
				{
					switch(System.IO.Path.GetExtension(file))
					{					
							
						case ".DAT":
							long filesize = new System.IO.FileInfo(gameFolder + @"\" + file).Length;
							if (filesize == 64000 || fileNameWithoutExtension == "TAKOVER" || fileNameWithoutExtension == "BUSTED" )
							{
								GetNode("BKG", "BACKGROUNDS").Nodes.Add(fileNameWithoutExtension, fileNameWithoutExtension + " (320x200)");
							}
							break;
							
						case ".TAB":
							if(allfiles.Contains(fileNameWithoutExtension + ".DAT"))
							{
								TreeNode node = GetNode("SPR", "SPRITES").Nodes.Add(fileNameWithoutExtension, fileNameWithoutExtension);
								node.Nodes.Add("");							
							}
							break;										
						
					}
				}
				if(fileNameWithoutExtension == "MSTA-0" /*|| fileNameWithoutExtension == "MUSSTA"*/)
			    {			   			   
					TreeNode node = GetNode("ANI", "ANIMATIONS").Nodes.Add(fileNameWithoutExtension, fileNameWithoutExtension);
					node.Nodes.Add("");			
			    }
				
				if(file.Substring(0, 3) == "SND" && System.IO.Path.GetExtension(file) == ".TAB")
				{
					TreeNode node = GetNode("SND", "SOUNDS").Nodes.Add(fileNameWithoutExtension, fileNameWithoutExtension + (fileNameWithoutExtension.Substring(fileNameWithoutExtension.Length - 1) == "0" ? " (8 bit)" : " (16 bit)"));
					node.Nodes.Add("");							
				}
				
				if(file.Substring(0, 5) == "MUSIC" && System.IO.Path.GetExtension(file) == ".TAB")
				{
					TreeNode node = GetNode("MUS", "MUSIC").Nodes.Add(fileNameWithoutExtension, fileNameWithoutExtension);
					node.Nodes.Add("");							
				}				
			}
			
		}
		
		
		
		void LoadPalette(string filename)
		{
			switch(filename)
			{			
				case "BUSTED":	
					Palette = SpriteViewer.GetPalette(gameFolder + @"\BUSPAL.DAT");	
					break;
					
				case "MAWAR0-0":
				case "MAWAR1-0":
				case "MCUP-0":
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MAWPAL-0.DAT");	
					break;
					
				case "MGLOBE-0":				
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MGLPAL-0.DAT");	
					break;
					
				case "MHAND-0":
				case "MNEG-0":
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MNGPAL-0.DAT");	
					break;
				/*				
				case "MIDLAND":		
					Palette = SpriteViewer.GetPalette(@"data\INPAL.DAT");	
					break;	
				*/	
				case "MRSSPR-0":
				case "MRES-0":				
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MRSPAL-0.DAT");	
					break;

				case "MSTATE-0":		
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MSTAP-0.DAT");	
					break;				
					
				case "MSTSPR-0":
				case "MSTOCK-0":		
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MSTPAL-0.DAT");	
					break;	
					
				case "TAKOVER":		
					Palette = SpriteViewer.GetPalette(gameFolder + @"\TAKPAL.DAT");	
					break;		
											
				default:
					Palette = SpriteViewer.GetPalette(gameFolder + @"\MPALETTE.DAT");
					break;
			}
		}
		
		
		void DisplaySprite(SpriteTab tab)
		{			
			if(tab.Width == 0 && tab.Height == 0) 
				return;
			
			pictureBox1.Width = tab.Width * zoom;
			pictureBox1.Height = tab.Height * zoom;
			
			Bitmap bmp = new Bitmap(tab.Width, tab.Height);			 	
						
			byte[] pixelData;
			int offset = 0;
			
			if(tab.Width == 320 && tab.Height == 200)
			{
				if(SpriteData.Length == 64034)
				{
					offset = 26;
				}
				else if(SpriteData.Length == 64033)
				{
					offset = 25;
				}
				pixelData = SpriteData;
			}
			else				
				pixelData = SpriteViewer.GetPixels(tab, SpriteData);
			
			DrawToBitmap(bmp, tab, pixelData, offset, 0, 0, false);
		    pictureBox1.Image = bmp;													
		}
			
		void DrawToBitmap(Bitmap bitmap, SpriteTab tab, byte[] pixelData, int offset, int posx, int posy, bool xflipped)
		{
			pictureBox1.Visible = true;
			for (int y = 0; y < tab.Height; y++)            
			{				
				for (int x = 0; x < tab.Width; x++)
				{		
					byte colorIndex;
					if(xflipped)
						colorIndex = pixelData[(tab.Width - 1 - x) + y * tab.Width + offset];
					else
						colorIndex = pixelData[x + y * tab.Width + offset];
										
					if(colorIndex != 255 || (tab.Width == 320 && tab.Height == 200))					
					{
						Color color = Color.FromArgb(Palette[colorIndex]);																				
					
						bitmap.SetPixel(x + posx, y + posy, color);
					}									
				}
			}							
		}
		
		void DrawSpriteRectangle(Bitmap bitmap, SpriteTab tab, int posx, int posy)
		{
			for(int x = 0; x < tab.Width; x++)
			{
				bitmap.SetPixel(x + posx, posy, Color.FromArgb(0, 255, 0));
				bitmap.SetPixel(x + posx, posy + tab.Height - 1, Color.FromArgb(0, 255, 0));
			}
			for(int y = 0; y < tab.Height; y++)
			{
				bitmap.SetPixel(posx, y + posy, Color.FromArgb(0, 255, 0));
				bitmap.SetPixel(posx + tab.Width - 1, y + posy, Color.FromArgb(0, 255, 0));
			}				
		}
	
		IEnumerable<SpriteFrame> GetAllFrames(SpriteFrame initialFrame)
		{
			SpriteFrame frame = initialFrame;
			yield return frame;
			
			while(SpriteFrames[frame.Next] != initialFrame)
			{
				frame = SpriteFrames[frame.Next];				
				yield return frame;
			}		
		}
		
		IEnumerable<SpriteElement> GetAllElements(SpriteElement element)
		{
			yield return element;
			while(element.Next != 0)
			{
				element = SpriteElements[element.Next];							    		
				yield return element;								
			}			
		}
		
		
		void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
		{
			timer1.Stop();			
			AudioPlayer.Stop();
			TreeNode node = e.Node;
			if (node != null )
			{
				if(node.Level == 0)
				{					
					pictureBox1.Visible = false;
				}
				else if(node.Level == 1)
				{
					if(node.Parent.Name == "BKG")
					{
						string filename = node.Name;						
						SpriteTabs = new[]{ new SpriteTab() { Offset = 0, Width = 320, Height = 200 }};				
						SpriteData = SpriteViewer.GetSpriteData(gameFolder + @"\" + filename +".DAT");									
						LoadPalette(filename);
						lastDatFileOpen = filename;
						DisplaySprite(SpriteTabs[0]);
					}														
					else
					{
						pictureBox1.Visible = false;
					}											
				}
				else if (node.Level == 2)
				{
					if(node.Parent.Parent.Name == "SPR")
					{
						string filename = node.Parent.Name;
						if(lastDatFileOpen == null || lastDatFileOpen != filename)
						{
							SpriteData = SpriteViewer.GetSpriteData(gameFolder + @"\" + filename +".DAT");	
							SpriteTabs = SpriteViewer.GetSpriteTabs(gameFolder + @"\" + filename +".TAB");
							LoadPalette(filename);	
							lastDatFileOpen = filename;
						}
								
						DisplaySprite(SpriteTabs[int.Parse(node.Name)]);
					}					
					else if(node.Parent.Parent.Name == "SND")
					{
						pictureBox1.Visible = false;
						string filename = node.Parent.Name;
						if(lastDatFileOpen == null || lastDatFileOpen != filename)
						{
							SoundData = SpriteViewer.GetSoundData(gameFolder + @"\" + filename +".DAT");	
							SoundTabs = SpriteViewer.GetSoundTabs(gameFolder + @"\" + filename +".TAB");							
							lastDatFileOpen = filename;
						}
						SoundTab tab = SoundTabs[int.Parse(node.Name)];
						if(filename.Substring(filename.Length - 1, 1) == "0")
							soundBytes = AudioPlayer.CreateWave(SoundData, tab.Offset, tab.Length, 8, 11025, 1);
						else
							soundBytes = AudioPlayer.CreateWave(SoundData, tab.Offset, tab.Length, 8, 22050, 1);
						AudioPlayer.Play(soundBytes, loopSound);
					}
					else if(node.Parent.Parent.Name == "ANI")
					{	
						int frame = int.Parse(node.Name);
						string filename = "MSPR-0";
						if(lastDatFileOpen == null || lastDatFileOpen != filename)
						{
							SpriteData = SpriteViewer.GetSpriteData(gameFolder + @"\" + filename +".DAT");								
							SpriteTabs = SpriteViewer.GetSpriteTabs(gameFolder + @"\" + filename +".TAB");
							LoadPalette(filename);	
							lastDatFileOpen = filename;
						}
						
						if(lastAniFileOpen == null || lastAniFileOpen != node.Name)
						{
							if(node.Name == "MSTA-0")
							{
								SpriteFrames = SpriteViewer.GetSpriteFrames(gameFolder + @"\MFRA-0.ANI");	
								SpriteElements = SpriteViewer.GetSpriteElements(gameFolder + @"\MELE-0.ANI");
							}
							else if(node.Name == "MUS")
							{
								SpriteFrames = SpriteViewer.GetSpriteFrames(gameFolder + @"\MUSFRA.ANI");	
								SpriteElements = SpriteViewer.GetSpriteElements(gameFolder + @"\MUSELE.ANI");
							}
							lastAniFileOpen = node.Name;
						}
						
						int minHeight = int.MaxValue;
						int minWidth = int.MaxValue;
						int maxHeight = int.MinValue;
						int maxWidth = int.MinValue;
						
						animationOffsetX = int.MinValue;
						animationOffsetY = int.MinValue;
												
						CurrentSpriteFrame = SpriteFrames[frame];
						FirstSpriteFrame = CurrentSpriteFrame;
						
						foreach(SpriteFrame spriteFrame in GetAllFrames(SpriteFrames[frame]))
				        {
							
							foreach(SpriteElement element in GetAllElements(SpriteElements[spriteFrame.First]))
							{
								SpriteTab tab = SpriteTabs[element.Sprite / 6];
								minWidth = Math.Min(minWidth, element.XOffset/2 +0);
								maxWidth = Math.Max(maxWidth, element.XOffset/2 + tab.Width);
								
								minHeight = Math.Min(minHeight, element.YOffset/2 + 0);								
								maxHeight = Math.Max(maxHeight, element.YOffset/2 + tab.Height);	

								animationOffsetX = Math.Max(animationOffsetX, -(element.XOffset/2));
								animationOffsetY = Math.Max(animationOffsetY, -(element.YOffset/2));																
							}
				        }													
						
						if((maxWidth - minWidth) > 0 && (maxHeight - minHeight) > 0)
						{
						    bitmapAnimation = new Bitmap(maxWidth - minWidth, maxHeight - minHeight);
							pictureBox1.Width = bitmapAnimation.Width * zoom;
							pictureBox1.Height = bitmapAnimation.Height * zoom;	
						}
						timer1.Start();
						Timer1Tick(this, EventArgs.Empty);
					}
					else
					{
						pictureBox1.Visible = false;						
					}	
				}
			}
		}
		
		
		
		void TreeView1BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			TreeNode node = e.Node;
			if (node != null)
			{				
				if(node.Level == 1 && node.Parent.Name == "SPR" && node.Nodes.Count == 1)
				{									
					node.Nodes.Clear();
					string filename = node.Name;							
					SpriteTabs = SpriteViewer.GetSpriteTabs(gameFolder + @"\" + filename +".TAB");
							
					int index = 0;
					foreach(SpriteTab tab in SpriteTabs)
					{						
						if(tab.Height != 0 && tab.Width != 0)
							node.Nodes.Add(index.ToString(), string.Format("SPRITE{0} ({1}x{2})", index, tab.Width, tab.Height));
						index++;
					}									
				}
				else if(node.Level == 1 && node.Parent.Name == "ANI" && node.Nodes.Count == 1)
				{
					node.Nodes.Clear();	
					int index = 0;	ushort[] frameIndexes = null;		
					if(node.Name == "MSTA-0")
					{
						SpriteFrames = SpriteViewer.GetSpriteFrames(gameFolder + @"\MFRA-0.ANI");	
						SpriteElements = SpriteViewer.GetSpriteElements(gameFolder + @"\MELE-0.ANI");
						SpriteTabs = SpriteViewer.GetSpriteTabs(gameFolder + @"\MSPR-0.TAB");
						frameIndexes = SpriteViewer.GetSpriteAnims(gameFolder + @"\MSTA-0.ANI");
					}
					else if(node.Name == "MUS")
					{
						SpriteFrames = SpriteViewer.GetSpriteFrames(gameFolder + @"\MUSFRA.ANI");	
						SpriteElements = SpriteViewer.GetSpriteElements(gameFolder + @"\MUSELE.ANI");
						SpriteTabs = SpriteViewer.GetSpriteTabs(gameFolder + @"\MUS.TAB");
						frameIndexes = SpriteViewer.GetSpriteAnims(gameFolder + @"\MUSSTA.ANI");
					}
						
					foreach(ushort frameIndex in frameIndexes)
					{							
						SpriteFrame spriteFrame = SpriteFrames[frameIndex];
											
						if(GetAllFrames(spriteFrame)
						   .Any(x => GetAllElements(SpriteElements[x.First])
						        .Select(y => SpriteTabs[y.Sprite / 6]).Any(y => y.Height > 0 && y.Width > 0)))
						{
							node.Nodes.Add(frameIndex.ToString(), string.Format("ANIM{0}", index));
						}
						index++;
					}
				}
				else if(node.Level == 1 && (node.Parent.Name == "SND" || node.Parent.Name == "MUS") && node.Nodes.Count == 1)
				{
					int index = 0;
					node.Nodes.Clear();					
					string filename = node.Name;	
					SoundTabs = SpriteViewer.GetSoundTabs(gameFolder + @"\" + filename +".TAB");							
					foreach(SoundTab snd in SoundTabs)
					{
						if(!string.IsNullOrEmpty(snd.Name) && snd.Name != "NULL.RAW")
						{
							node.Nodes.Add(index.ToString(), snd.Name);
						}
						index++;
					}					
				}
			}
		}
		
		void ZoomToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			foreach(ToolStripItem t in zoomToolStripMenuItem.DropDownItems)
			{
				if(t != e.ClickedItem)
					((ToolStripMenuItem)t).Checked = false;
			}
			zoom = int.Parse((string)e.ClickedItem.Tag);
			TreeView1AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
		}
		
		
		
		void Timer1Tick(object sender, EventArgs e)
		{	
			using (Graphics g = Graphics.FromImage(bitmapAnimation))
			{
				g.Clear(Color.Empty);
			}
			
			if(CurrentSpriteFrame.Width > 0 && CurrentSpriteFrame.Height > 0)
			{												
				foreach(SpriteElement element in GetAllElements(SpriteElements[CurrentSpriteFrame.First]))
				{
					SpriteTab tab = SpriteTabs[element.Sprite / 6];
					DrawToBitmap(bitmapAnimation, tab, SpriteViewer.GetPixels(tab, SpriteData), 0, 
				          animationOffsetX +   element.XOffset/2,
				          animationOffsetY  +    element.YOffset/2, (element.XFlipped & 0x1) == 0x1);
				}
				if(showAnimationElementsToolStripMenuItem.Checked)
				{
					foreach(SpriteElement element in GetAllElements(SpriteElements[CurrentSpriteFrame.First]))
					{
						SpriteTab tab = SpriteTabs[element.Sprite / 6];
						DrawSpriteRectangle(bitmapAnimation, tab, 
					          animationOffsetX +   element.XOffset/2,
					          animationOffsetY  +    element.YOffset/2);
					}
				}							
				pictureBox1.Image = bitmapAnimation;					
			}			
			
			
			CurrentSpriteFrame = SpriteFrames[CurrentSpriteFrame.Next];		
		}
		
		void exitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		
		void ExportToPNGToolStripMenuItemClick(object sender, System.EventArgs e)
		{
			TreeNode node = treeView1.SelectedNode;
			if(node != null)
			{
				if((node.Level == 2 && node.Parent.Parent.Name == "SPR") || (node.Level == 1 && node.Parent.Name == "BKG"))
				{
					SaveFileDialog dialog = new SaveFileDialog();	
					if(node.Parent.Name == "BKG")
						dialog.FileName = node.Name.ToLower() + ".png";
					else
						dialog.FileName = node.Parent.Name.ToLower() + node.Name + ".png";			
					dialog.Filter = "PNG (*.png)|*.png";
					if (dialog.ShowDialog() == DialogResult.OK)
					{
					   pictureBox1.Image.Save(dialog.FileName, ImageFormat.Png);
					}
				}
				else if(node.Level == 2  && node.Parent.Parent.Name == "SND")
				{			
					SaveFileDialog dialog = new SaveFileDialog();	
					dialog.FileName = node.Text.Split('.')[0].ToLower() + ".wav";
					dialog.Filter = "WAV (*.wav)|*.wav";
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						File.WriteAllBytes(dialog.FileName, soundBytes);			   
					}
				}
				else if(node.Level == 2  && node.Parent.Parent.Name == "MUS")
				{			
					SaveFileDialog dialog = new SaveFileDialog();	
					dialog.FileName = node.Text.ToLower();
					string extension = node.Text.Split('.')[1];
					dialog.Filter = string.Format("{0} file (*.{1})|*.{1}", extension, extension.ToLower());
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						string filename = node.Parent.Name;
						if(lastDatFileOpen == null || lastDatFileOpen != filename)
						{
							SoundData = SpriteViewer.GetSoundData(gameFolder + @"\" + filename +".DAT");	
							SoundTabs = SpriteViewer.GetSoundTabs(gameFolder + @"\" + filename +".TAB");							
							lastDatFileOpen = filename;
						}
						SoundTab tab = SoundTabs[int.Parse(node.Name)];
						
						File.WriteAllBytes(dialog.FileName, SoundData.Skip((int)tab.Offset).Take((int)tab.Length).ToArray());
					}
				}
				else if(node.Level == 2 && node.Parent.Parent.Name == "ANI")
				{
					SaveFileDialog dialog = new SaveFileDialog();	
					dialog.FileName = node.Parent.Name.ToLower() + ".png";
					dialog.Filter = "PNG individual frames (*.png)|*.png";
					if (dialog.ShowDialog() == DialogResult.OK)
					{
						int index = 0;
						foreach(SpriteFrame frame in GetAllFrames(SpriteFrames[FirstSpriteFrame.Next]))
						{
							if(frame.Width > 0 && frame.Height > 0)
							{
								using (Graphics g = Graphics.FromImage(bitmapAnimation))
								{
									g.Clear(Color.Empty);
								}
								
								foreach(SpriteElement element in GetAllElements(SpriteElements[frame.First]))
								{
									SpriteTab tab = SpriteTabs[element.Sprite / 6];
									DrawToBitmap(bitmapAnimation, tab, SpriteViewer.GetPixels(tab, SpriteData), 0, 
								          animationOffsetX +   element.XOffset/2,
								          animationOffsetY  +    element.YOffset/2, (element.XFlipped & 0x1) == 0x1);
								}								
								pictureBox1.Image.Save(System.IO.Path.GetDirectoryName(dialog.FileName)
								                       + @"\" + System.IO.Path.GetFileNameWithoutExtension(dialog.FileName) + index.ToString("0000") + ".png", ImageFormat.Png);
							}
							index++;							
						}
					}
				}
			}
		}
		
		void SetGameLocationFolderToolStripMenuItemClick(object sender, EventArgs e)
		{			
			FolderBrowserDialog dialog = new FolderBrowserDialog();			
			dialog.Description = "Please select folder where game is installed :";
			DialogResult result = dialog.ShowDialog();
						
			if (result == DialogResult.OK)
			{
				 treeView1.Nodes.Clear();
			     gameFolder = dialog.SelectedPath;
			     if(System.IO.Directory.Exists(gameFolder + @"/data"))
			        gameFolder += @"/data"; 
			     LoadTreeView();
			}
			
		}
		
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show(this.Text+" v1.02\r\nDate : 25.01.2014.\r\nContact me : tigrou.ind@gmail.com");
		}
		
		
		void UseBilinearFilteringToolStripMenuItemClick(object sender, EventArgs e)
		{
			pictureBox1.InterpolationMode = pictureBox1.InterpolationMode == InterpolationMode.NearestNeighbor ?
				InterpolationMode.Bilinear : InterpolationMode.NearestNeighbor;
			pictureBox1.Refresh();
		}
		
		
		void LoopSoundToolStripMenuItemClick(object sender, EventArgs e)
		{
			loopSound = !loopSound;
			AudioPlayer.Stop();
			if(loopSound)
				TreeView1AfterSelect(this, new TreeViewEventArgs(treeView1.SelectedNode));
		}
	}
}
