//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens.Saml2
{
    /// <summary>
    /// A security token backed by a SAML2 assertion.
    /// </summary>
    public class Saml2SecurityToken : SecurityToken
    {
        private Saml2Assertion assertion;

        /// <summary>
        /// Initializes an instance of <see cref="Saml2SecurityToken"/> from a <see cref="Saml2Assertion"/>.
        /// </summary>
        /// <param name="assertion">A <see cref="Saml2Assertion"/> to initialize from.</param>
        public Saml2SecurityToken(Saml2Assertion assertion)
        {
            if (null == assertion)
                throw LogHelper.LogArgumentNullException(nameof(assertion));

            this.assertion = assertion;
        }

        /// <summary>
        /// Gets the <see cref="Saml2Assertion"/> for this token.
        /// </summary>
        public Saml2Assertion Assertion
        {
            get { return this.assertion; }
        }

        /// <summary>
        /// Gets the SecurityToken id.
        /// </summary>
        public override string Id
        {
            get { return this.assertion.Id.Value; }
        }

        /// <summary>
        /// Gets the <see cref="SecurityToken"/> of the issuer.
        /// </summary>
        public SecurityToken IssuerToken
        {
            get; set;
        }

        /// <summary>
        /// Gets the collection of <see cref="SecurityKey"/> contained in this token.
        /// </summary>
        public override SecurityKey SecurityKey
        {
            get;
        }

        /// <summary>
        /// Gets the time the token is valid from.
        /// </summary>
        public override DateTime ValidFrom
        {
            get
            {
                if (null != this.assertion.Conditions && null != this.assertion.Conditions.NotBefore)
                    return this.assertion.Conditions.NotBefore.Value;
                else
                    return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the time the token is valid to.
        /// </summary>
        public override DateTime ValidTo
        {
            get
            {
                if (null != this.assertion.Conditions && null != this.assertion.Conditions.NotOnOrAfter)
                    return this.assertion.Conditions.NotOnOrAfter.Value;
                else
                    return DateTime.MaxValue;
            }
        }

        public override string Issuer
        {
            // TODO - is this right, Saml2NameIdentifier is a complex type
            get { return assertion.Issuer.Value; }
        }

        public override SecurityKey SigningKey
        {
            get; set;
        }
    }
}