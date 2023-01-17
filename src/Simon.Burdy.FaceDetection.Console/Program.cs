
using System.Text.Json;
using Simon.Burdy.FaceDetection;

namespace massyl.zelleg.FaceDetection.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var imagePaths = args.ToList();
            var imageData = new List<byte[]>();
            foreach (var imagePath in imagePaths)
            {
                var imageBytes = File.ReadAllBytes(imagePath);
                imageData.Add(imageBytes);
            }

            var faceDetection = new FaceDetection();
            var detectFaceInScenesResults = faceDetection.DetectInScenes(imageData);
            foreach (var detectionResult in detectFaceInScenesResults)
            {
                Console.WriteLine($"Points: {JsonSerializer.Serialize(detectionResult.Points)}");
            }
        }
    }
}