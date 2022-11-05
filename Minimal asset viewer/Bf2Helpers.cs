using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFP4FExplorerWV;

namespace Minimal_asset_viewer
{
    internal static class Bf2Helpers
    {
        private static string GetIndentString(int indentRequired = 0)
        {
            string indent = "";
            for (int i = 0; i < indentRequired; i++)
            {
                indent += '\t';
            }
            return indent;
        }

        internal static string GetDescription(this Helper.BF2MeshBMGeometryMaterial material, int indentRequired = 0)
        {
            var rVal = "";
            rVal += $"numMaterials {material.numMaterials}\n";
            int onMat = 0;
            var indent = GetIndentString(indentRequired);

            foreach(var mat in material.materials)
            {
                rVal += $"{indent}MATERIAL {onMat++}\n";
                rVal += $"{indent}alphaMode {mat.alphaMode}\n";
                rVal += $"{indent}indiciesStartIndex {mat.indiciesStartIndex}\n";
                rVal += $"{indent}numIndicies {mat.numIndicies}\n";
                rVal += $"{indent}numTextureMaps {mat.numTextureMaps}\n";
                rVal += $"{indent}numVertices {mat.numVertices}\n";
                rVal += $"{indent}shaderFile '{mat.shaderFile}'\n";
                rVal += $"{indent}technique '{mat.technique}'\n";
                rVal += $"{indent}textureMapFiles: [{mat.textureMapFiles.Aggregate("",(acc, next) => acc+next+", ")}]\n";
                rVal += $"{indent}u1 {mat.u1}\n";
                rVal += $"{indent}u2 {mat.u2}\n";
                rVal += $"{indent}u3 {mat.u3}\n";
                rVal += $"{indent}vertexStartIndex {mat.vertexStartIndex}\n\n";


            }
            return rVal;
        }

        internal static string GetDescription(this Helper.BF2MeshSTMGeometryMaterial material, int indentRequired = 0)
        {
            var rVal = "";
            var indent = GetIndentString(indentRequired);
            rVal += $"{indent}numMaterials {material.numMaterials}\n";
            int onMat = 0;

            foreach (var mat in material.materials)
            {
                rVal += $"{indent}MATERIAL {onMat++}\n";
                rVal += $"{indent}alphaMode {mat.alphaMode}\n";
                rVal += $"{indent}indiciesStartIndex {mat.indiciesStartIndex}\n";
                rVal += $"{indent}numIndicies {mat.numIndicies}\n";
                rVal += $"{indent}numTextureMaps {mat.numTextureMaps}\n";
                rVal += $"{indent}numVertices {mat.numVertices}\n";
                rVal += $"{indent}shaderFile '{mat.shaderFile}'\n";
                rVal += $"{indent}technique '{mat.technique}'\n";
                rVal += $"{indent}textureMapFiles: [{mat.textureMapFiles.Aggregate("", (acc, next) => acc + next + ", ")}]\n";
                rVal += $"{indent}u1 {mat.u1}\n";
                rVal += $"{indent}u2 {mat.u2}\n";
                rVal += $"{indent}u3 {mat.u3}\n";
                rVal += $"{indent}u4 x: {mat.u4.x} y: {mat.u4.y} y: {mat.u4.z}\n";
                rVal += $"{indent}u5 x: {mat.u5.x} y: {mat.u5.y} y: {mat.u5.z}\n";

                rVal += $"{indent}vertexStartIndex {mat.vertexStartIndex}\n\n";


            }

            return rVal;
        }

        internal static string GetDescription(this Helper.BF2MeshGeometry geometry, int indentRequired = 0)
        {
            var indent = GetIndentString(indentRequired);

            var rVal = "";
            rVal += $"{indent}(mesh.geometry)\n";
            rVal += $"{indent}vertexFormat: {geometry.vertexFormat}\n";
            rVal += $"{indent}vertexStride: {geometry.vertexStride}\n";
            rVal += $"{indent}numGeom: {geometry.numGeom}\n";
            rVal += $"{indent}numIndices: {geometry.numIndices}\n";
            rVal += $"{indent}numLods: {geometry.numLods}\n";
            rVal += $"{indent}numVertexElements: {geometry.numVertexElements}\n";
            rVal += $"{indent}numVertices: {geometry.numVertices}\n\n";
            return rVal;
        }
        internal static string GetDescription(this BF2BundledMesh mesh)
        {
            var rVal = "";
            //var header = mesh.header;
            var geometry = mesh.geometry;
            rVal += geometry.GetDescription();

            var lods = mesh.lods;
            rVal += "(mesh.lods)\n";
            rVal += $"size = {mesh.lods.Count}\n\n";
            
            var u1 = mesh.u1;
            rVal += "u1\n\n";

            var geomat = mesh.geomat;
            rVal += "(mesh.geomat)\n";
            int onMat = 0;
            foreach(var mat in geomat)
            {
                rVal += $"Material {onMat++}\n";
                rVal += mat.GetDescription(1);
            }

            return rVal;
        }
        internal static string GetDescription(this BF2StaticMesh mesh)
        {
            var rVal = "";
            //var header = mesh.header;
            var geometry = mesh.geometry;
            rVal += geometry.GetDescription();

            var lods = mesh.lods;
            rVal += "(mesh.lods)\n";
            rVal += $"size = {mesh.lods.Count}\n\n";

            var u1 = mesh.u1;
            rVal += "u1\n\n";

            var geomat = mesh.geomat;
            rVal += "(mesh.geomat)\n";
            int onMat = 0;
            foreach (var mat in geomat)
            {
                rVal += $"Material {onMat++}\n";
                rVal += mat.GetDescription(1);
            }

            return rVal;
        }
    }
}
