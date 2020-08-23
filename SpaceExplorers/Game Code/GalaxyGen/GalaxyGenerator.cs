using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Distributions;
using SpaceExplorers.Game_Code.Constants;
using AnoleEngine.Engine_Base;
using AnoleEngine.Engine_Base.Game_Code.GalaxyGen;
using SpaceExplorers.States;

namespace SpaceExplorers.Game_Code.Galaxy
{
    class GalaxyGenerator
    {
        public static int StarCount { get; set; }
        public static Star[] Stars { get; set; }
        public static GlobalConstants.GalaxySize SizeEnum { get; set; }
        public static GlobalConstants.GalaxyType GalaxyTypeEnum { get; set; }

        public static float GalaxyMajorRadius { get; set; }
        public static float GalaxyMinorRadius { get; set; }
        public static int HomeStarIndex { get; private set; }

        private static Random random = new Random();

        public static void GenerateStars(GlobalConstants.GalaxySize GalaxySize, GlobalConstants.GalaxyType GalaxyType)
        {      
            SizeEnum = GalaxySize;
            GalaxyTypeEnum = GalaxyType;

            SetGalaxyRadiusAndStarCountByGalazySize(SizeEnum);

            Engine.Instance.WriteToConsole("Generating galaxy!");
            Engine.Instance.WriteToConsole("Galaxy size: " + SizeEnum + "; Galaxy type: " + GalaxyTypeEnum);
            Engine.Instance.WriteToConsole("Star count: " + StarCount.ToString());
            Engine.Instance.WriteToConsole("");

            List<Star> arrStars = new List<Star>();         
            ContinuousUniform StarRadiusGen = new ContinuousUniform(2, 16.3);

            for (int i = 0; i < StarCount; i++)
            {
                float fltStarRad = (float)StarRadiusGen.Sample();

                bool blnShouldCreateStar = true;
                Vector2f StarPosition = new Vector2f(0,0);

                if (GalaxyTypeEnum == GlobalConstants.GalaxyType.DISK)
                {
                    StarPosition = GetStarCoordinatesForElipticalGalaxy(ref arrStars, out blnShouldCreateStar);
                }
                else if (GalaxyTypeEnum == GlobalConstants.GalaxyType.RING)
                {
                    StarPosition = GetStarCoordinatesForRingGalaxy(ref arrStars, out blnShouldCreateStar);
                }
                else
                {
                    StarPosition = GetStarCoordinatesForElipticalGalaxy(ref arrStars, out blnShouldCreateStar);
                }

                if (blnShouldCreateStar && StarPosition.X != 0 && StarPosition.Y != 0)
                {
                    Star objStar = new Star(fltStarRad, StarPosition, 1);
                    arrStars.Add(objStar);
                }

                Engine.Instance.WriteToConsole("Stars generated: " + i.ToString(), true, false, false);
            }

            Stars = arrStars.ToArray();
            HomeStarIndex = random.Next(0, Stars.Length - 1);

            while (Stars[HomeStarIndex].HasPanets == false)
            {
                if (Stars[HomeStarIndex].HasPanets == true)
                {
                    Stars[HomeStarIndex].Home = true;
                    Stars[HomeStarIndex].HasPlayer = true;
                }
                else
                {
                    HomeStarIndex = random.Next(0, Stars.Length - 1);
                }
            }
        }

        [Obsolete()]
        private static void SetStartTypeAndColorByRadius(Star objStar, float fltRadius)
        {
            if (fltRadius < 8)
            {
                objStar.StarType = GlobalConstants.StarType.K;
                objStar._Color = new Color(244, 79, 65);
            }
            else if (fltRadius >= 8 && fltRadius < 12)
            {
                objStar.StarType = GlobalConstants.StarType.M;
                objStar._Color = new Color(244, 166, 65);
            }
            else if (fltRadius >= 12 && fltRadius < 15)
            {
                objStar.StarType = GlobalConstants.StarType.G;
                objStar._Color = new Color(244, 238, 65);
                objStar.Body.Radius -= 5;
            }
            else if (fltRadius >= 15 && fltRadius < 15.85)
            {
                objStar.StarType = GlobalConstants.StarType.F;
                objStar._Color = new Color(193, 65, 244);
                objStar.Body.Radius -= 5;
            }
            else if (fltRadius >= 15.85 && fltRadius < 16)
            {
                objStar.StarType = GlobalConstants.StarType.A;
                objStar._Color = new Color(65, 211, 244);
            }
            else if (fltRadius >= 16.1 && fltRadius < 16.25)
            {
                objStar.StarType = GlobalConstants.StarType.B;
                objStar._Color = new Color(66, 244, 212);
                objStar.Body.Radius += 5;
            }
            else if (fltRadius > 16.25 && fltRadius < 16.3)
            {
                objStar.StarType = GlobalConstants.StarType.O;
                objStar._Color = new Color(66, 244, 217);
                objStar.Body.Radius += 10;
            }
            else
            {
                objStar._Color = new Color(66, 244, 217);
                objStar.StarType = GlobalConstants.StarType.Unknown; 
                objStar.Body.Radius += 10;
            }
        }

        private static float CalculateMinorRadius()
        {
            if (GalaxyTypeEnum == GlobalConstants.GalaxyType.OVAL)
            {
                return (float)(GalaxyMajorRadius * random.NextDouble());
            }
            else if (GalaxyTypeEnum == GlobalConstants.GalaxyType.RING)
            {
                return (float)(GalaxyMajorRadius / 2);
            }
            else
            {
                return GalaxyMajorRadius;
            }
        }
        
        private static void SetGalaxyRadiusAndStarCountByGalazySize(GlobalConstants.GalaxySize Size)
        {
            switch (Size)
            {
                case GlobalConstants.GalaxySize.GALACTIC:
                    GalaxyMajorRadius = 100000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(3000, 4000);
                    break;
                case GlobalConstants.GalaxySize.HUGE:
                    GalaxyMajorRadius = 75000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(1500, 2500);
                    break;
                case GlobalConstants.GalaxySize.LARGE:
                    GalaxyMajorRadius = 50000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(1000, 1300);
                    break;
                case GlobalConstants.GalaxySize.MEDIUM:
                    GalaxyMajorRadius = 35000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(850, 1000);
                    break;
                case GlobalConstants.GalaxySize.SMALL:
                    GalaxyMajorRadius = 17000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(550, 850);
                    break;
                case GlobalConstants.GalaxySize.XSMALL:
                    GalaxyMajorRadius = 10000;
                    GalaxyMinorRadius = CalculateMinorRadius();
                    StarCount = random.Next(100, 250);
                    break;
                default:
                    break;
            }
        }

        // ELIPTICAL GALAXY
        private static Vector2f GetStarCoordinatesForElipticalGalaxy(ref List<Star> arrStars, out bool blnShouldBuildStar)
        {
            int intOrbitRadius = (int)Normal.Sample(GalaxyMajorRadius, GalaxyMajorRadius / 2);
            int THETA = (int)ContinuousUniform.Sample(0, 359);

            double XCartCoord = intOrbitRadius * Math.Cos(THETA);
            double YCartCoord = intOrbitRadius * Math.Sin(THETA);
            Vector2f StarCoordinate = new Vector2f((float)XCartCoord, (float)YCartCoord);

            for (int currStarIndex = 0; currStarIndex < arrStars.Count(); currStarIndex++)
            {
                if (arrStars[currStarIndex] != null)
                {
                    float DistBetweenStars = (float)Math.Sqrt(((StarCoordinate.X - arrStars[currStarIndex].Origin.X) * (StarCoordinate.X - arrStars[currStarIndex].Origin.X))
                                                + ((StarCoordinate.Y - arrStars[currStarIndex].Origin.Y) * (StarCoordinate.Y - arrStars[currStarIndex].Origin.Y)));

                    if (DistBetweenStars < 50 )
                    {
                        blnShouldBuildStar = false;
                        return new Vector2f();
                    }
                }
            }

            blnShouldBuildStar = true;
            return StarCoordinate;          
        }

        // RING GALAXY
        private static Vector2f GetStarCoordinatesForRingGalaxy(ref List<Star> arrStars, out bool blnShouldBuildStar)
        {
            int intOrbitRadius = (int)ContinuousUniform.Sample(GalaxyMinorRadius, GalaxyMajorRadius);
            int THETA = (int)ContinuousUniform.Sample(0, 359);

            double XCartCoord = intOrbitRadius * Math.Cos(THETA);
            double YCartCoord = intOrbitRadius * Math.Sin(THETA);

            Vector2f StarCoordinate = new Vector2f((float)XCartCoord, (float)YCartCoord);

            for (int currStarIndex = 0; currStarIndex < arrStars.Count(); currStarIndex++)
            {
                if (arrStars[currStarIndex] != null)
                {
                    float DistBetweenStars = (float)Math.Sqrt(((StarCoordinate.X - arrStars[currStarIndex].Origin.X) * (StarCoordinate.X - arrStars[currStarIndex].Origin.X))
                                                + ((StarCoordinate.Y - arrStars[currStarIndex].Origin.Y) * (StarCoordinate.Y - arrStars[currStarIndex].Origin.Y)));

                    if (DistBetweenStars < 50)
                    {
                        blnShouldBuildStar = false;
                        return new Vector2f();
                    }
                }
            }

            blnShouldBuildStar = true;
            return StarCoordinate;
        }

        public static void GenerateBodies()
        {

        }

    }
}
