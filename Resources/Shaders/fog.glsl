#version 330

// Input vertex attributes (from vertex shader)
out vec3 fragPosition;
out vec2 fragTexCoord;
out vec4 fragColor;
out vec3 fragNormal;

// Input uniform values
uniform sampler2D texture0;

// Output fragment color
out vec4 fColor;

// NOTE: Add your custom variables here
uniform vec4 ambient;
uniform vec3 viewPos;
uniform vec4 fogColor;
uniform float fogDensity;

void main()
{
    // Texel color fetching from texture sampler
    vec4 texelColor = texture(texture0, fragTexCoord);

    // NOTE: Implement here your fragment shader code
    float dist = length(viewPos-fragPosition);
    float fogFactor = 1.0/exp((dist*fogDensity)*(dist*fogDensity));
    fogFactor = clamp(fogFactor,0.0,1.0);
  
    vec4 finalColor = texelColor * ambient;
    fcolor = mix(fogColor,finalColor,fogFactor);
}