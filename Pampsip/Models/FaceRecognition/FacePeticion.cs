using System;
using System.IO;

namespace Pampsip.Models.FaceRecognition
{
	public class Detect
    {
        public Stream Stream { get; set; }        
		public bool returnFaceId { get; set; }
		public bool returnFaceLandmarks { get; set; }
		public string returnFaceAttributes { get; set; }
		public string parametros { get { return "returnFaceId=" + returnFaceId + "&returnFaceLandmarks=" + returnFaceLandmarks/*+ "&returnFaceAttributes=" + returnFaceAttributes*/; } }
    }

	public class PeticionIdentify
    {        
		public string largePersonGroupId { get; set; }
		public Guid[] faceIds { get; set; }
		public int maxNumOfCandidatesReturned { get; set; }
		public double confidenceThreshold { get; set; }
    }

	public class Identify
	{
		public PeticionIdentify peticion { get; set; }
		public string parametros { get { return "largePersonGroupId=" + peticion.largePersonGroupId + "&faceIds=" + peticion.faceIds + "&maxNumOfCandidatesReturned=" + peticion.maxNumOfCandidatesReturned + "&confidenceThreshold=" + peticion.confidenceThreshold; } }
	}

	public class Person
    {
		public string personGroupId { get; set; }
		public Guid personId { get; set; }        
		public string parametros { get { return personGroupId + "/persons/" + personId; } }
    }

	public class PersonResponse
    {
		public Guid PersonId { get; set; }
		public string Name { get; set; }
		public string UserData { get; set; }
    }

	public class FaceResponse
	{
		public Guid FaceId { get; set; }
		public FaceRectangle FaceRectangle { get; set; }
	}

	public class IdentifyResponse
    {
		public Guid FaceId { get; set; }
		public Candidate[] Candidates { get; set; }
    }

    public class Candidate
    {
		public Guid PersonId { get; set; }
		public double Confidence { get; set; }
    }

	public class FaceRectangle
    {
		public int width { get; set; }
		public int height { get; set; }
		public int left { get; set; }
		public int top { get; set; }
    }
    
	public class FaceAttributes
    {        
		public double Age { get; set; }
		public string Gender { get; set; }
		public double Smile { get; set; }
		public Glasses Glasses { get; set; }
    }

	public enum Glasses
    {
        NoGlasses,
        Sunglasses,
        ReadingGlasses,  
        SwimmingGoggles
    }

	public enum FaceAttributeType
    {        
        Age,
        Gender,
        //FacialHair,
        Smile,
        //HeadPose,
        Glasses,
    }
}
