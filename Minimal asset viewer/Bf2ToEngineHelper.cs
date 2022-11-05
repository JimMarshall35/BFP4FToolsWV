using BFP4FExplorerWV;
using Minimal_asset_viewer.new_importer;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_asset_viewer
{
    static class Bf2ToEngineHelper
    {
        private static RenderObject.VertexWired GetVector(this bf2mesh mesh, int pos)
        {
            return new RenderObject.VertexWired(new Vector4(mesh.vert[pos], mesh.vert[pos + 1], mesh.vert[pos + 2], 1f), Color4.Black);
        }

        private static RenderObject.VertexTextured GetVertex(this bf2mesh mesh, int pos)
        {
            var posVertAttrib = mesh.FindVertAttribByUsage(VertexUsage.POSITION);
            var uvVertAttrib = mesh.FindVertAttribByUsage(VertexUsage.TEXCOORD);
            return new RenderObject.VertexTextured(
                new Vector4(mesh.vert[pos + (posVertAttrib.offset / sizeof(float))], mesh.vert[pos + (posVertAttrib.offset / sizeof(float)) + 1], mesh.vert[pos + (posVertAttrib.offset / sizeof(float)) + 2], 1f),
                Color.White,
                new Vector2(mesh.vert[pos + (uvVertAttrib.offset / sizeof(float))],
                mesh.vert[pos + (uvVertAttrib.offset / sizeof(float))+1]));
        }
        public static List<RenderObject> ConvertForEngine(this bf2mesh mesh, Engine3D engine, bool loadTextures, int lod)
        {
            List<RenderObject> result = new List<RenderObject>();
            for(int j = 0; j<mesh.geomnum; j++)
            {
                if (lod >= mesh.geom[j].lodnum)
                {
                    lod = mesh.geom[j].lodnum - 1;
                }
                var matnum = mesh.geom[j].lod[lod].matnum;
                for (int i = 0; i < matnum; i++)
                {
                    var mat = mesh.geom[j].lod[lod].mat[i];
                    var list = new List<RenderObject.VertexTextured>();
                    var list2 = new List<RenderObject.VertexWired>();
                    Texture2D texture = null;
                    if (loadTextures)
                        foreach (string path in mat.map)
                        {
                            texture = engine.textureManager.FindTextureByPath(path);
                            if (texture != null)
                                break;
                        }
                    if (texture == null)
                        texture = engine.defaultTexture;

                    var m = mesh.vertstride / sizeof(float);

                    for (int k = 0; k < mat.inum; k++)
                    {
                        int pos = (mesh.index[(int)mat.istart + k] + (int)mat.vstart) * (int)m;
                        list.Add(mesh.GetVertex(pos));
                        list2.Add(mesh.GetVector(pos));
                    }
                    if (mat.inum != 0)
                    {
                        RenderObject o = new RenderObject(engine.device, RenderObject.RenderType.TriListTextured, texture, engine);
                        o.verticesTextured = list.ToArray();
                        o.InitGeometry();
                        result.Add(o);
                        RenderObject o2 = new RenderObject(engine.device, RenderObject.RenderType.TriListWired, texture, engine);
                        o2.verticesWired = list2.ToArray();
                        o2.InitGeometry();
                        result.Add(o2);
                    }
                }
            }
            
            return result;
            /*
             
            List<RenderObject> result = new List<RenderObject>();
            if (geoMatIdx >= geomat.Count)
                geoMatIdx = geomat.Count() - 1;
            Helper.BF2MeshSTMGeometryMaterial lod0 = geomat[geoMatIdx];
            for (int i = 0; i < lod0.numMaterials; i++)
            {
                Helper.BF2MeshSTMMaterial mat = lod0.materials[i];                
                List<RenderObject.VertexTextured> list = new List<RenderObject.VertexTextured>();
                List<RenderObject.VertexWired> list2 = new List<RenderObject.VertexWired>();
                Texture2D texture = null;
                if(loadTextures)
                    foreach (string path in mat.textureMapFiles)
                    {
                        texture = engine.textureManager.FindTextureByPath(path);
                        if (texture != null)
                            break;
                    }
                if(texture == null)
                    texture = engine.defaultTexture;
                int m = COMPACTED_VERT_SIZE_IN_FLOATS;
                for (int j = 0; j < mat.numIndicies; j++)
                {
                    int pos = (geometry.indices[(int)mat.indiciesStartIndex + j] + (int)mat.vertexStartIndex) * m;
                    list.Add(GetVertex(pos));
                    list2.Add(GetVector(pos));
                }
                if (mat.numIndicies != 0)
                {
                    RenderObject o = new RenderObject(engine.device, RenderObject.RenderType.TriListTextured, texture, engine);
                    o.verticesTextured = list.ToArray();
                    o.InitGeometry();
                    result.Add(o);
                    RenderObject o2 = new RenderObject(engine.device, RenderObject.RenderType.TriListWired, texture, engine);
                    o2.verticesWired = list2.ToArray();
                    o2.InitGeometry();
                    result.Add(o2);
                }
            }
            return result;
             
             */
        }
    }
}
