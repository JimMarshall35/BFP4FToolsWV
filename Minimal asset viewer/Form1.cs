using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BFP4FExplorerWV;

namespace Minimal_asset_viewer
{
    public partial class Form1 : Form
    {
        private readonly Engine3D _engine;

        private bool _meshMouseUp = false;
        private Point _meshLastMousePos = new Point(0, 0);
        private readonly string _meshesFolderPath;
        private readonly string[] _meshFiles;
        private int _selectedFileIndex = 0;

        private string FullPath => Path.Combine(_meshesFolderPath, _meshFiles[_selectedFileIndex]);

        public Form1(string[] args)
        {
            InitializeComponent();
            _engine = new Engine3D(pictureBox1);
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            _meshesFolderPath = new Uri(Path.Combine(path, "meshes")).LocalPath;
            _meshFiles = Directory.GetFiles(_meshesFolderPath)
                .Select(s => Path.GetFileName(s))
                .ToArray();
            MeshFilesCombo.DataSource = _meshFiles;
            MeshFilesCombo.SelectedIndex = 0;

            LoadMeshAtPath(FullPath);
            rendererTimer.Enabled = true;
        }

        private bool LoadMeshAtPath(string path, bool resetCamera = true)
        {

            _engine.objects.Clear();
            string ending = Path.GetExtension(path).ToLower();
            var data = File.ReadAllBytes(path);
            switch (ending)
            {
                case ".staticmesh":
                    {
                        var newMesh = new_importer.Bf2Loader.LoadBf2File(path, error => Console.WriteLine(error));
                        geomatIdxPicker.Maximum = newMesh.MaxLodNum - 1;
                        _engine.objects.AddRange(newMesh.ConvertForEngine(_engine, true, (int)geomatIdxPicker.Value));
                        newMesh.SetTreeView(meshTreeView);
                        break;
                    }
                    
                case ".bundledmesh":
                    {

                        var newMesh = new_importer.Bf2Loader.LoadBf2File(path, error => Console.WriteLine(error));
                        geomatIdxPicker.Maximum = newMesh.MaxLodNum - 1;
                        _engine.objects.AddRange(newMesh.ConvertForEngine(_engine, true, (int)geomatIdxPicker.Value));
                        newMesh.SetTreeView(meshTreeView);
                        break;
                    }
                    
                case ".skinnedmesh":
                    {
                        var newMesh = new_importer.Bf2Loader.LoadBf2File(path, error => Console.WriteLine(error));
                        geomatIdxPicker.Maximum = newMesh.MaxLodNum - 1;

                        _engine.objects.AddRange(newMesh.ConvertForEngine(_engine, true, (int)geomatIdxPicker.Value));
                        newMesh.SetTreeView(meshTreeView);
                        break;
                    }
                    
                case ".collisionmesh":
                    {
                        var newMesh = new_importer.Bf2Loader.LoadBf2File(path, error => Console.WriteLine(error));
                        BF2CollisionMesh cm = new BF2CollisionMesh(data);
                        _engine.objects.AddRange(newMesh.ConvertForEngine(_engine, true, (int)geomatIdxPicker.Value));
                        break;
                    }
                    
                default:
                    RenderObject o = new RenderObject(_engine.device, RenderObject.RenderType.TriListWired, _engine.defaultTexture, _engine);
                    o.InitGeometry();
                    _engine.objects.Add(o);
                    break;
            }
            if (resetCamera)
            {
                _engine.ResetCameraDistance();
            }
            
            return true;
        }

        private void rendererTimer_Tick(object sender, EventArgs e)
        {
            _engine.Render();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _meshMouseUp = false;
            _meshLastMousePos = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _meshMouseUp = true;
            _meshLastMousePos = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_meshMouseUp)
            {
                int dx = e.X - _meshLastMousePos.X;
                int dy = e.Y - _meshLastMousePos.Y;
                _engine.CamDis *= 1 + (dy * 0.01f);
                _engine.CamRot += dx * 0.01f;
                _meshLastMousePos = e.Location;
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (_engine != null)
            {
                _engine.Resize(pictureBox1);
            }
        }

        


        private void MeshFilesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedFileIndex = ((ComboBox)sender).SelectedIndex;

            LoadMeshAtPath(FullPath);
        }


        private void geomatIdxPicker_ValueChanged(object sender, EventArgs e)
        {
            LoadMeshAtPath(FullPath, false);
        }
    }
}
