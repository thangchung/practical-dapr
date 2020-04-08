using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.WebUI.Host
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public partial class Queries
        : global::StrawberryShake.IDocument
    {
        private readonly byte[] _hashName = new byte[]
        {
            109,
            100,
            53,
            72,
            97,
            115,
            104
        };
        private readonly byte[] _hash = new byte[]
        {
            97,
            101,
            97,
            55,
            56,
            100,
            57,
            98,
            57,
            57,
            48,
            49,
            97,
            57,
            53,
            49,
            100,
            50,
            56,
            52,
            48,
            51,
            53,
            55,
            56,
            101,
            102,
            56,
            56,
            98,
            53,
            98
        };
        private readonly byte[] _content = new byte[]
        {
            113,
            117,
            101,
            114,
            121,
            32,
            103,
            101,
            116,
            80,
            114,
            111,
            100,
            117,
            99,
            116,
            115,
            32,
            123,
            32,
            112,
            114,
            111,
            100,
            117,
            99,
            116,
            115,
            40,
            112,
            97,
            103,
            101,
            58,
            32,
            49,
            44,
            32,
            112,
            97,
            103,
            101,
            83,
            105,
            122,
            101,
            58,
            32,
            57,
            41,
            32,
            123,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            101,
            100,
            103,
            101,
            115,
            32,
            123,
            32,
            95,
            95,
            116,
            121,
            112,
            101,
            110,
            97,
            109,
            101,
            32,
            110,
            97,
            109,
            101,
            32,
            105,
            110,
            118,
            101,
            110,
            116,
            111,
            114,
            121,
            76,
            111,
            99,
            97,
            116,
            105,
            111,
            110,
            32,
            125,
            32,
            116,
            111,
            116,
            97,
            108,
            67,
            111,
            117,
            110,
            116,
            32,
            125,
            32,
            125
        };

        public ReadOnlySpan<byte> HashName => _hashName;

        public ReadOnlySpan<byte> Hash => _hash;

        public ReadOnlySpan<byte> Content => _content;

        public static Queries Default { get; } = new Queries();

        public override string ToString() => 
            @"query getProducts {
              products(page: 1, pageSize: 9) {
                edges {
                  name
                  inventoryLocation
                }
                totalCount
              }
            }";
    }
}
