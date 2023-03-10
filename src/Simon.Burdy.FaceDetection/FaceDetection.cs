using System.Reflection;
using OpenCvSharp;

namespace Simon.Burdy.FaceDetection;

public class FaceDetection
{  public IList<FaceDetectionResult> DetectInScenes(IList<byte[]>
imagesSceneData)
 {
// TODO implement your code here
 throw new NotImplementedException();
 }
 
 
 private FaceDetectionResult FaceDetectionInScene(byte[] imageScr)
 {
  var pathClassifier = Path.Combine(GetExecutingPath(),
   "haarcascade_frontalface_default.xml");
  using var cascade = new CascadeClassifier(pathClassifier);
  Mat result;
  using var src = Mat.FromImageData(imageScr);
  using var gray = new Mat();
  result = src.Clone();
  Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
  // Detect faces
  var faces = cascade.DetectMultiScale(
   gray, 2, 2, HaarDetectionTypes.ScaleImage, new Size(50, 50));
  // Render all detected faces
  foreach (var face in faces)
  {
   var center = new Point
   {
    X = (int)(face.X + face.Width * 0.5),
    Y = (int)(face.Y + face.Height * 0.5)
   };
   var axes = new Size
   {
    Width = (int)(face.Width * 0.5),
    Height = (int)(face.Height * 0.5)
   };
   Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(255, 0, 255), 4);}
  var imageResult = result.ToBytes();
  return new FaceDetectionResult
  {
   ImageData = imageResult,
   Points = faces.Select(point => new ObjectDetectionPoint { X = point.X, Y =
    point.Y }).ToList()
  };
 } 
 private static string GetExecutingPath()
 {
  var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
  var executingPath = Path.GetDirectoryName(executingAssemblyPath);
  return executingPath;
 }

}