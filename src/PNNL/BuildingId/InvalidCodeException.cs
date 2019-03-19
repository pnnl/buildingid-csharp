// Copyright (c) 2019, Battelle Memorial Institute
// All rights reserved.
//
// 1. Battelle Memorial Institute (hereinafter Battelle) hereby grants permission
//    to any person or entity lawfully obtaining a copy of this software and
//    associated documentation files (hereinafter "the Software") to redistribute
//    and use the Software in source and binary forms, with or without
//    modification.  Such person or entity may use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and may permit
//    others to do so, subject to the following conditions:
//
//    * Redistributions of source code must retain the above copyright notice, this
//      list of conditions and the following disclaimers.
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation
//      and/or other materials provided with the distribution.
//    * Other than as used herein, neither the name Battelle Memorial Institute or
//      Battelle may be used in any form whatsoever without the express written
//      consent of Battelle.
//
// 2. THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL BATTELLE OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
// DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
// OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
// ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

namespace PNNL.BuildingId
{

    /// <summary>
    /// The exception that is thrown when a <see cref="PNNL.BuildingId.Code"/> is invalid.
    /// </summary>
    [System.Serializable()]
    public class InvalidCodeException : System.Exception
    {

        /// <summary>
        /// The UBID code.
        /// </summary>
        public Code Code { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.InvalidCodeException"/> class
        /// with a specified UBID code.
        /// <summary>
        /// <param name="code">The UBID code.</param>
        public InvalidCodeException(Code code) : base()
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.InvalidCodeException"/> class
        /// with a specified UBID code and error message.
        /// <summary>
        /// <param name="code">The UBID code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InvalidCodeException(Code code, string message) : base(message)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.InvalidCodeException"/> class
        /// with a specified UBID code, error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// <summary>
        /// <param name="code">The UBID code.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public InvalidCodeException(Code code, string message, System.Exception inner) : base(message, inner)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PNNL.BuildingId.InvalidCodeException"/> class
        /// with serialized data.
        /// <summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected InvalidCodeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.Code = (Code) info.GetValue("Code", typeof(Code));
        }

        /// <summary>
        /// Populates a <cref name="System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            info.AddValue("Code", this.Code);

            base.GetObjectData(info, context);
        }

    }

}
