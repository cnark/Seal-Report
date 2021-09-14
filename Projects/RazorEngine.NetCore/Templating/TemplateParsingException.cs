﻿namespace RazorEngine.Templating
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using System.Security;
#if RAZOR4
#else
    using System.Web.Razor.Parser.SyntaxTree;
#endif

    /// <summary>
    /// Defines an exception that occurs during template parsing.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors"), Serializable]
    public class TemplateParsingException : Exception
    {
        #region Constructors
        /// <summary>
        /// Initialises a new instance of <see cref="TemplateParsingException"/>.
        /// </summary>
        /// <param name="errorMessage">The error message generated by the parser.</param>
        /// <param name="characterIndex">The character index of the error.</param>
        /// <param name="lineIndex">The line index of the error.</param>
        internal TemplateParsingException(string errorMessage, int characterIndex, int lineIndex)
            : base(string.Format ("({0}:{1}) - {2}", lineIndex, characterIndex, errorMessage))
        {
            Column = characterIndex;
            Line = lineIndex;
        }

        /// <summary>
        /// Initialises a new instance of <see cref="TemplateParsingException"/> from serialised data.
        /// </summary>
        /// <param name="info">The serialisation info.</param>
        /// <param name="context">The streaming context.</param>
        protected TemplateParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Column = info.GetInt32("Column");
            Line = info.GetInt32("Line");
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the column the parsing error occured.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// Gets the line the parsing error occured.
        /// </summary>
        public int Line { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the object data for serialisation.
        /// </summary>
        /// <param name="info">The serialisation info.</param>
        /// <param name="context">The streaming context.</param>
        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Column", Column);
            info.AddValue("Line", Line);
        }
        #endregion
    }
}