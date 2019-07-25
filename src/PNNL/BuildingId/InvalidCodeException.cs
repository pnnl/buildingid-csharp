// Copyright (c) 2019, Battelle Memorial Institute
// All rights reserved.
//
// See LICENSE.txt and WARRANTY.txt for details.

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
