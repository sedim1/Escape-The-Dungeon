#version 330

// Input vertex attributes (from vertex shader)
in vec2 fragTexCoord;
in vec4 fragColor;

// Input uniform values
uniform sampler2D texture0;
uniform vec4 colorReplacement;

// Output fragment color
out vec4 finalColor;

void main()
{
    vec4 texelColor = texture2D(texture0, fragTexCoord);
    if(texelColor.r == 0.0 && texelColor.g == 1.0 && texelColor.b == 0.0) {
        finalColor = colorReplacement;
    }
    else{
        finalColor = texelColor;
    }
}