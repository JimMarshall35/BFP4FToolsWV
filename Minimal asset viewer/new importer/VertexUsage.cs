using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimal_asset_viewer.new_importer
{
    public enum VertexUsage
    {
        POSITION = 0,
        BLENDWEIGHT,   // 1
        BLENDINDICES,  // 2
        NORMAL,        // 3
        PSIZE,         // 4
        TEXCOORD,      // 5
        TANGENT,       // 6
        BINORMAL,      // 7
        TESSFACTOR,    // 8
        POSITIONT,     // 9
        COLOR,         // 10
        FOG,           // 11
        DEPTH,         // 12
        SAMPLE,        // 13

        NUMVALS,
        UNKNOWNTYPE
    }

    public enum VertexType
    {
        FLOAT1 = 0,
        FLOAT2,
        FLOAT3,
        FLOAT4,
        D3DCOLOR,
        UBYTE4,
        SHORT2,
        SHORT4,
        NUMVALS,
        UNKNOWNTYPE
    }
}
