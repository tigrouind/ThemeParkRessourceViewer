using System;
using System.Collections.Generic;
using System.IO;

namespace test
{
	/// <summary>
	/// Description of Tab.
	/// </summary>	
	class SpriteTab
	{
	    public uint Offset;
	    public short Width;
	    public short Height;
	}
	
	class SpriteFrame
	{	
		public ushort First;
		public byte Width;
		public byte Height;
		public ushort Flags;
		public ushort Next;	
	}
	
	class SpriteElement
	{
		public ushort Sprite;
		public short  XOffset;
		public short  YOffset;
		public ushort XFlipped;
		public ushort Next;
	};
		
	class SoundTab
	{
		public string Name;	
		public uint Offset;
		public uint Length;
	}

	class SpriteViewer
	{
		public static SpriteTab[] GetSpriteTabs(string filename)
		{
			List<SpriteTab> list = new List<SpriteTab>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
				    SpriteTab tab = new SpriteTab();
			        tab.Offset = reader.ReadUInt32();
			        tab.Width = reader.ReadByte();
			        tab.Height = reader.ReadByte();
			        list.Add(tab);
				}
		    }		
			return list.ToArray();
		}
		
		public static ushort[] GetSpriteAnims(string filename)
		{
			List<ushort> list = new List<ushort>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
					ushort indexes = reader.ReadUInt16();
			        list.Add(indexes);
				}
		    }		
			return list.ToArray();		
		}
		
		public static byte[] GetSpriteData(string filename)
		{
			return File.ReadAllBytes(filename);			
		}
			
		public static int[] GetPalette(string filename)
		{
			List<int> list = new List<int>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
					int palette;
					palette = 255 << 24 | (reader.ReadByte() * 4) << 16 | (reader.ReadByte() * 4) << 8 | (reader.ReadByte() * 4);
			    	list.Add(palette);
				}
		    }	
			return list.ToArray();
		}
		
		public static SpriteFrame[] GetSpriteFrames(string filename)
		{
			List<SpriteFrame> list = new List<SpriteFrame>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
				    SpriteFrame frame = new SpriteFrame();
				    frame.First = reader.ReadUInt16();
			        frame.Width = reader.ReadByte();
			        frame.Height = reader.ReadByte();
			        frame.Flags = reader.ReadUInt16();
			        frame.Next = reader.ReadUInt16();
			        list.Add(frame);
				}
		    }		
			return list.ToArray();
		}
		
		public static SpriteElement[] GetSpriteElements(string filename)
		{
			List<SpriteElement> list = new List<SpriteElement>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
				    SpriteElement element = new SpriteElement();
				    element.Sprite = reader.ReadUInt16();
			        element.XOffset = reader.ReadInt16();
			        element.YOffset = reader.ReadInt16();
			        element.XFlipped = reader.ReadUInt16();
			        element.Next = reader.ReadUInt16();
			        list.Add(element);
				}
		    }		
			return list.ToArray();
		}
		
		public static byte[] GetSoundData(string filename)
		{
			return File.ReadAllBytes(filename);			
		}
			
		
		public static SoundTab[] GetSoundTabs(string filename)
		{
			List<SoundTab> list = new List<SoundTab>();
			using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
	    	{
				while(reader.BaseStream.Position != reader.BaseStream.Length)
				{
				    SoundTab tab = new SoundTab();
				    tab.Name = System.Text.Encoding.ASCII.GetString(reader.ReadBytes(12)).TrimEnd('\0');
			        reader.ReadBytes(6); //?
			        tab.Offset = reader.ReadUInt32();
			        reader.ReadBytes(4); //?
			        tab.Length = reader.ReadUInt32();
			        reader.ReadBytes(2); //?
			        list.Add(tab);
				}
		    }		
			return list.ToArray();
		}
		
			
		public static byte[] GetPixels(SpriteTab tab, byte[] data)
		{
			uint sprite_data = tab.Offset;
			byte[] pixel_data = new byte[tab.Width * tab.Height]; 

			for (int i = 0; i < tab.Height; ++i)
			{
			    int current_pixel = i * tab.Width;			
			    sbyte run_length = unchecked((sbyte)data[sprite_data++]);
			    while (run_length != 0)
			    {
			        if (run_length > 0)
			        {               
			        	// pixel run
			            for (int j = 0; j < run_length; j++)
			            	pixel_data[current_pixel++] = data[sprite_data++];						    
			        }
			        else if (run_length < 0)
			        {     
			        	// transparent run			
			            run_length *= -1;
			            for (int j = 0; j < run_length; j++)
			            	pixel_data[current_pixel++] = 255;			           
			        }
			        else if (run_length == 0)
			        {      
			        	// end of the row			        	
			        }
			
			        run_length = unchecked((sbyte)data[sprite_data++]);
			    }
			    while(current_pixel < (i+1) * tab.Width)
			            	pixel_data[current_pixel++] = 255;	
			}
			return pixel_data;
		}
	}
}
