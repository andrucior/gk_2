**Bézier Surface Visualization and Rendering Tool**
This project is a 3D rendering application designed for visualizing and manipulating cubic Bézier surfaces. It supports generating a triangular mesh for the surface, applying transformations, and rendering with dynamic lighting and textures.

**Features**

1. Surface input
Reads a cubic Bézier surface defined by 16 control points from a text file.

2. Triangular Mesh Generation
Dynamically generates a triangular mesh through interpolation.
Adjustable mesh precision via a user-controlled slider.


3. Real-time rotation around X and Z axes with adjustable angles.
Projection of the rotated surface onto the XY plane for rendering.

4. Rendering Modes
Wireframe or solid-filled triangle rendering.
Smooth shading using barycentric interpolation for vertex normals.

5. Dynamic Lighting
Implements Lambertian and specular reflection models.
Adjustable material properties (diffuse, specular, shininess) via UI sliders.
Animated light source moving along a spiral path.

6. Texture and Normal Mapping
Supports applying textures and normal maps to enhance surface detail.
Allows interactive modification of surface normals using a loaded normal map.
