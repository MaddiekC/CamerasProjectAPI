using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LibVLCSharp.Shared;
using CamerasProjectAPI.Models;
using System.Drawing.Printing;
using OpenCvSharp;
using System.IO.Pipelines;

namespace CamerasProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamController : ControllerBase
    {
        private readonly BodycamContext _context;
        private static LibVLC _libVLC;


        public StreamController(BodycamContext context)
        {
            _context = context;
            _libVLC = new LibVLC();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStream(int id)
        {
            var camara = await _context.Camaras.FindAsync(id);

            if (camara == null)
            {
                return NotFound();
            }

            string username = "admin";
            string password = "admin1234";

            string rtspUrl = $"rtsp://{username}:{password}@{camara.Ip}:7070";
            var ffmpegProcess = new System.Diagnostics.Process();
            ffmpegProcess.StartInfo.FileName = @"C:\Users\Kenny Recalde\Desktop\CamerasProjectAPI\CamerasProjectAPI\ffmpeg\ffmpeg-win64.exe";
            ffmpegProcess.StartInfo.Arguments = $"-rtsp_transport tcp -i {rtspUrl} -c:v mjpeg -f mjpeg -";
            ffmpegProcess.StartInfo.RedirectStandardOutput = true;
            ffmpegProcess.StartInfo.RedirectStandardError = true;
            ffmpegProcess.StartInfo.UseShellExecute = false;
            ffmpegProcess.StartInfo.CreateNoWindow = true;
            ffmpegProcess.Start();

            ffmpegProcess.ErrorDataReceived += (sender, e) => Console.WriteLine($"FFmpeg error: {e.Data}");
            ffmpegProcess.BeginErrorReadLine();

            Response.ContentType = "multipart/x-mixed-replace; boundary=frame";

            var buffer = new byte[4096];
            int bytesRead;
            using (var stream = ffmpegProcess.StandardOutput.BaseStream)
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    Console.WriteLine($"Leyendo {bytesRead} bytes del stream");
                    await Response.Body.WriteAsync(System.Text.Encoding.ASCII.GetBytes("--frame\r\n"));
                    await Response.Body.WriteAsync(System.Text.Encoding.ASCII.GetBytes("Content-Type: image/jpeg\r\n\r\n"));
                    await Response.Body.WriteAsync(buffer, 0, bytesRead);
                    await Response.Body.WriteAsync(System.Text.Encoding.ASCII.GetBytes("\r\n"));
                    await Response.Body.FlushAsync();
                }
            }

            ffmpegProcess.WaitForExit();
            return new EmptyResult();
        }
    }
}