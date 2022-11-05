using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Minimal_asset_viewer.new_importer;
using System.IO;
using System.Reflection;

namespace JimsTests
{
    /// <summary>
    /// Tests the behavior of the new importer
    /// classes are the same as the bfMeshViewer vb code they're based on.
    /// 
    /// test cases derived from comparing bfMeshViewer log output to vb source code
    /// </summary>
    [TestFixture]
    class NewImporterTests
    {
        private static string TestMeshesFolderPath => new Uri(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "meshes")).LocalPath;
        private static string StaticMeshTestFilePath => Path.Combine(TestMeshesFolderPath, "medievalhouse_01.staticmesh");

        private static object[] HeaderTestCasesSource() => new object[]
        {
            //string path, uint expectedu1, uint expectedversion, uint expectedu3, uint expectedu4, uint expectedu5
            new object[] { StaticMeshTestFilePath, 0u, 11u, 0u, 0u, 0u }
        };

        [TestCaseSource(nameof(HeaderTestCasesSource))]
        public void Bf2Loader_TestStaticMeshLoaded_HeaderMatchKnown(string path, uint expectedu1, uint expectedversion, uint expectedu3, uint expectedu4, uint expectedu5)
        {
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(expectedu1, loaded.head.u1);
            Assert.AreEqual(expectedversion, loaded.head.version);
            Assert.AreEqual(expectedu3, loaded.head.u3);
            Assert.AreEqual(expectedu4, loaded.head.u4);
            Assert.AreEqual(expectedu5, loaded.head.u5);
        }

        private static object[] GeomTableTestCasesSource() => new object[]
        {
            //string path, uint expectedGeomCount, uint[] expectedLodNumbers
            new object[] { StaticMeshTestFilePath, 1u, new int[] { 4 } }
        };

        [TestCaseSource(nameof(GeomTableTestCasesSource))]
        public void Bf2Loader_TestStaticMeshLoaded_GeomTableMatchKnown(string path, uint expectedGeomCount, uint[] expectedLodNumbers)
        {
            Assert.That(expectedGeomCount == expectedLodNumbers.Length, "invalid test case");
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(1, loaded.geom.Count());
            Assert.AreEqual(1, loaded.geomnum);
            for(int i=0; i<expectedGeomCount; i++)
            {
                Assert.AreEqual(expectedLodNumbers[i], loaded.geom[i].lodnum);
            }
            
        }

        private static object[] AttribBlockTestCaseSources() => new object[]
        {
            //string path, int numVertAttribs, uint[][] expectedValues
            new object[]
            {
                StaticMeshTestFilePath,
                7,
                new uint[][]
                {
                    // flag, offset, vartype, usage
                    new uint[] {0,0,2,0},
                    new uint[] {0,12,2,3},
                    new uint[] {0,24,4,2},
                    new uint[] {0,28,1,5},
                    new uint[] {0,36,1,261},
                    new uint[] {0,44,2,6},
                    new uint[] {255,0,17,0},

                }
            }
        };

        [TestCaseSource(nameof(AttribBlockTestCaseSources))]
        public void Bf2Loader_TestStaticMeshLoaded_AttribBlockMatchKnown(string path, int numVertAttribs, uint[][] expectedValues)
        {
            Assert.True(numVertAttribs == expectedValues.Length, "Invalid test setup");
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(numVertAttribs, loaded.vertattribnum);

            for(int i = 0; i < numVertAttribs; i++)
            {
                var attrib = loaded.vertattrib[i];
                uint[] thisAttribVals = expectedValues[i];
                Assert.True(thisAttribVals.Length == 4, "invalid test setup");
                Assert.AreEqual(thisAttribVals[0], attrib.flag);
                Assert.AreEqual(thisAttribVals[1], attrib.offset);
                Assert.AreEqual(thisAttribVals[2], attrib.vartype);
                Assert.AreEqual(thisAttribVals[3], attrib.usage);

            }
        }
        private static object[] VertexBlockTestCaseSources() => new object[]
        {
            //string path, uint vertformat, uint vertstride, uint vertnum
            new object[] { StaticMeshTestFilePath, 4u, 56u, 9300u }
        };

        [TestCaseSource(nameof(VertexBlockTestCaseSources))]
        public void Bf2Loader_TestStaticMeshLoaded_VertexBlockMatchKnown(string path, uint vertformat, uint vertstride, uint vertnum)
        {
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(vertformat, loaded.vertformat);
            Assert.AreEqual(vertstride, loaded.vertstride);
            Assert.AreEqual(vertnum, loaded.vertnum);
        }

        private static object[] IndexBlockTestCasesSource() => new object[]
        {
            //string path, uint indexnum
            new object[] { StaticMeshTestFilePath, 17031u }
        };

        [TestCaseSource(nameof(IndexBlockTestCasesSource))]
        public void Bf2Loader_TestStaticMeshLoaded_IndexBlockMatchKnown(string path, uint indexnum)
        {
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(indexnum, loaded.indexnum);
        }

        private static object[] U2ValTestCasesSource() => new object[]
        {
            new object[] { StaticMeshTestFilePath, 8u }
        };

        [TestCaseSource(nameof(U2ValTestCasesSource))]
        public void Bf2Loader_TestStaticMeshLoaded_u2MatchKnown(string path, uint u2Val)
        {
            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));
            Assert.AreEqual(u2Val, loaded.u2);
        }

        private static object[] NodeChunksTestCasesSource() => new object[]
        {
            new object[] { StaticMeshTestFilePath, 1u, new uint[] { 4 }, new uint[][] { new uint[] { 2,1,1,1} } },
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="numGeom"></param>
        /// <param name="numLods"> num lods for each geom</param>
        /// <param name="numNodes"> num nodes for each geom and lod </param>
        [TestCaseSource(nameof(NodeChunksTestCasesSource))]
        public void Bf2Loader_TestStaticMeshLoaded_NodesChunkMatchesKnown(string path, uint numGeom, uint[] numLods, uint[][] numNodes)
        {
            Assert.True(numGeom == numLods.Length, "Invalid test case");
            Assert.True(numGeom == numNodes.Length, "Invalid test case");
            for(int i=0; i < numGeom; i++)
            {
                Assert.True(numLods[i] == numNodes[i].Length, "Invalid test case");
            }

            var loaded = Bf2Loader.LoadBf2File(path, error => Assert.Fail(error));

            for(int i = 0; i < numGeom; i++)
            {
                for(int j=0; j < numLods[i]; j++)
                {
                    Assert.AreEqual(numNodes[i][j], loaded.geom[i].lod[j].nodenum);
                    Assert.NotNull(loaded.geom[i].lod[j].node);
                }
            }
        }

        [Test]
        public void Bf2Loader_TestStaticMeshLoaded_GeomBlockMatchesKnown()
        {
            var loaded = Bf2Loader.LoadBf2File(StaticMeshTestFilePath, error => Assert.Fail(error));

            var lod0 = loaded.geom[0].lod[0];
            Assert.AreEqual(1, lod0.matnum);
            var lod0mat = lod0.mat[0];
            Assert.AreEqual(0, lod0mat.alphamode);
            Assert.AreEqual("StaticMesh.fx", lod0mat.fxfile);
            Assert.AreEqual("Base", lod0mat.technique);
            Assert.AreEqual(1, lod0mat.mapnum);
            Assert.AreEqual("objects/staticobjects/textures/medievalhouse_01.dds", lod0mat.map[0]);
            Assert.AreEqual(0, lod0mat.vstart);
            Assert.AreEqual(0, lod0mat.istart);
            Assert.AreEqual(7524, lod0mat.inum);
            Assert.AreEqual(3588, lod0mat.vnum);

            var lod1 = loaded.geom[0].lod[1];
            Assert.AreEqual(1, lod1.matnum);
            var lod1mat = lod1.mat[0];
            Assert.AreEqual(0, lod1mat.alphamode);
            Assert.AreEqual("StaticMesh.fx", lod1mat.fxfile);
            Assert.AreEqual("Base", lod1mat.technique);
            Assert.AreEqual(1, lod1mat.mapnum);
            Assert.AreEqual("objects/staticobjects/textures/medievalhouse_01.dds", lod1mat.map[0]);
            Assert.AreEqual(3588, lod1mat.vstart);
            Assert.AreEqual(7524, lod1mat.istart);
            Assert.AreEqual(4215, lod1mat.inum);
            Assert.AreEqual(2391, lod1mat.vnum);

            var lod2 = loaded.geom[0].lod[2];
            Assert.AreEqual(1, lod2.matnum);
            var lod2mat = lod2.mat[0];
            Assert.AreEqual(0, lod2mat.alphamode);
            Assert.AreEqual("StaticMesh.fx", lod2mat.fxfile);
            Assert.AreEqual("Base", lod2mat.technique);
            Assert.AreEqual(1, lod2mat.mapnum);
            Assert.AreEqual("objects/staticobjects/textures/medievalhouse_01.dds", lod2mat.map[0]);
            Assert.AreEqual(5979, lod2mat.vstart);
            Assert.AreEqual(11739, lod2mat.istart);
            Assert.AreEqual(2961, lod2mat.inum);
            Assert.AreEqual(1850, lod2mat.vnum);

            var lod3 = loaded.geom[0].lod[3];
            Assert.AreEqual(1, lod3.matnum);
            var lod3mat = lod3.mat[0];
            Assert.AreEqual(0, lod3mat.alphamode);
            Assert.AreEqual("StaticMesh.fx", lod3mat.fxfile);
            Assert.AreEqual("Base", lod3mat.technique);
            Assert.AreEqual(1, lod3mat.mapnum);
            Assert.AreEqual("objects/staticobjects/textures/medievalhouse_01.dds", lod3mat.map[0]);
            Assert.AreEqual(7829, lod3mat.vstart);
            Assert.AreEqual(14700, lod3mat.istart);
            Assert.AreEqual(2331, lod3mat.inum);
            Assert.AreEqual(1471, lod3mat.vnum);
        }
    }
}
