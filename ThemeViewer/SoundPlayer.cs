using System;
using System.IO;
using System.Media;

namespace test
{
	
	public class AudioPlayer
	{
		public static byte[] CreateWave(byte [] data_ByteArray, uint offset, uint length, ushort bitsSample, uint sampleRate, ushort channels)
	    {	    	
	        string header_GroupID = "RIFF";  // RIFF
	        uint header_FileLength = 0;      // total file length minus 8, which is taken up by RIFF
	        string header_RiffType = "WAVE"; // always WAVE
	
	        string fmt_ChunkID = "fmt "; // Four bytes: "fmt "
	        uint fmt_ChunkSize = 16;     // Length of header in bytes
	        ushort fmt_FormatTag = 1;        // 1 for PCM
	        ushort fmt_Channels = channels;         // Number of channels, 2=stereo
	        uint fmt_SamplesPerSec = sampleRate;  // sample rate
	        ushort fmt_BitsPerSample = bitsSample;   // bits per sample
	        ushort fmt_BlockAlign =
	            (ushort)(fmt_Channels * (fmt_BitsPerSample / 8)); // sample frame size, in bytes
	        uint fmt_AvgBytesPerSec =
	            fmt_SamplesPerSec * fmt_BlockAlign; // for estimating RAM allocation
	
	        string data_ChunkID = "data";  // "data"
	        uint data_ChunkSize;           // Length of header in bytes
	        
	
	        // Calculate file and data chunk size in bytes
	        data_ChunkSize = (uint)(length * (fmt_BitsPerSample / 8));
	        header_FileLength = 4 + (8 + fmt_ChunkSize) + (8 + data_ChunkSize);
	
	        // write data to a MemoryStream with BinaryWriter
	        using(MemoryStream audioStream = new MemoryStream())	       
        	using(BinaryWriter writer = new BinaryWriter(audioStream))
	        {		
		        // Write the header
		        writer.Write(header_GroupID.ToCharArray());
		        writer.Write(header_FileLength);
		        writer.Write(header_RiffType.ToCharArray());
		
		        // Write the format chunk
		        writer.Write(fmt_ChunkID.ToCharArray());
		        writer.Write(fmt_ChunkSize);
		        writer.Write(fmt_FormatTag);
		        writer.Write(fmt_Channels);
		        writer.Write(fmt_SamplesPerSec);
		        writer.Write(fmt_AvgBytesPerSec);
		        writer.Write(fmt_BlockAlign);
		        writer.Write(fmt_BitsPerSample);
		
		        // Write the data chunk
		        writer.Write(data_ChunkID.ToCharArray());
		        writer.Write(data_ChunkSize);
		        for(int i = 0 ; i < length ; i++) 
		        {
		        	writer.Write(data_ByteArray[i + offset]);
		        }
		        
		        return audioStream.ToArray();
	        }
	    }
	    
		public static void Play(byte[] audioBytes, bool loop)
	    {
			using(SoundPlayer player = new SoundPlayer(new MemoryStream(audioBytes)))
	        {
	        	// rewind stream
		        player.Stream.Seek(0, SeekOrigin.Begin);
		        if(loop)		        
		        	player.PlayLooping();
		        else
		        	player.Play();
	        }			
	    }
		
		public static void Stop()
		{
			using(SoundPlayer player = new SoundPlayer())
	        {
				player.Stop();
	        }	
		}
	}
}
