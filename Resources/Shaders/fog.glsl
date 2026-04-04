#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec4 fragColor;
in vec3 fragPosition;

// Input uniform values
uniform sampler2D texture0;
uniform vec4 colDiffuse;

// Output fragment color
out vec4 finalColor;

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
    float dist = length;
  
    finalColor = texelColor*colDiffuse*fragColor;
}