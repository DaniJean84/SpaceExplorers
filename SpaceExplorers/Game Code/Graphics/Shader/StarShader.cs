namespace AnoleEngine.Engine_Base.Game_Code.Graphics.Shader
{
    public class StarShader
    {
        public static string VertexShader = "void main()"+
                                            "{"+
	                                            "gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;"+
	                                            "gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;"+
	                                            "gl_FrontColor = gl_Color;"+
                                            "}";

        //public static string FragShader = "uniform vec4 color;"+
        //                                    "uniform float expand;" +
        //                                    "uniform vec2 center;" +
        //                                    "uniform float radius;" +
        //                                    "void main(void)" +
        //                                    "{" +
        //                                    "vec2 centerFromSfml = vec2(center.x, center.y);" +
        //                                    "vec2 p = (gl_FragCoord.xy - centerFromSfml) / radius;" +
        //                                        "float r = sqrt(dot(p, p));" +
        //                                        "if (r < 1.0)" +
        //                                        "{" +
        //                                            "gl_FragColor = mix(color, gl_Color, (r - expand) / (1 - expand));" +
        //                                        "}" +
        //                                        "else" +
        //                                        "{" +
        //                                            "gl_FragColor = gl_Color;" +
        //                                        "}" +
        //                                    "}";


        public const string FragShader = "uniform vec2 frag_LightOrigin;"+
                                        "uniform vec3 frag_LightColor;"+
                                        "uniform float frag_LightAttenuation;" +
                                        "uniform vec2 frag_ScreenResolution;" +
                                        "void main(){" +
                                        "       vec2 baseDistance =  gl_FragCoord.xy;" +
                                        "       baseDistance.y = frag_ScreenResolution.y-baseDistance.y;" +
                                        "       vec2 distance=frag_LightOrigin - baseDistance;" +
                                        "       float linear_distance = length(distance);" +
                                        "       float attenuation=1.0/( frag_LightAttenuation*linear_distance + frag_LightAttenuation*linear_distance);" +
                                        "       vec4 lightColor = vec4(frag_LightColor, 1.0);" +
                                        "       vec4 color = vec4(attenuation, attenuation, attenuation, 1.0) * lightColor; gl_FragColor=color;}";
    }
}
