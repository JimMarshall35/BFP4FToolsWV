using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BFP4FExplorerWV;

namespace Minimal_asset_viewer
{
    public partial class Form1 : Form
    {

        public Form1(string[] args)
        {
            InitializeComponent();
            _engine = new Engine3D(pictureBox1);
            LoadMeshAtPath(args[0]);
            rendererTimer.Enabled = true;
        }

        private bool LoadMeshAtPath(string path)
        {
            string ending = Path.GetExtension(path).ToLower();
            var data = File.ReadAllBytes(path);
            switch (ending)
            {
                case ".staticmesh":
                    BF2StaticMesh stm = new BF2StaticMesh(data);
                    _engine.objects.AddRange(stm.ConvertForEngine(_engine, true, 1));
                    break;
                case ".bundledmesh":
                    BF2BundledMesh bm = new BF2BundledMesh(data);
                    _engine.objects.AddRange(bm.ConvertForEngine(_engine, true, 1));
                    break;
                case ".skinnedmesh":
                    BF2SkinnedMesh skm = new BF2SkinnedMesh(data);
                    _engine.objects.AddRange(skm.ConvertForEngine(_engine, true, 1));
                    break;
                case ".collisionmesh":
                    BF2CollisionMesh cm = new BF2CollisionMesh(data);
                    _engine.objects.AddRange(cm.ConvertForEngine(_engine));
                    break;
                default:
                    RenderObject o = new RenderObject(_engine.device, RenderObject.RenderType.TriListWired, _engine.defaultTexture, _engine);
                    o.InitGeometry();
                    _engine.objects.Add(o);
                    break;
            }
            _engine.ResetCameraDistance();
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

        private readonly Engine3D _engine;

        private bool _meshMouseUp = false;
        private Point _meshLastMousePos = new Point(0, 0);

        
    }
}
