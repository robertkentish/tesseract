using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Tesseract.Interop;

namespace Tesseract
{
    public class PdfRenderer : DisposableBase
    {
        private static readonly TraceSource trace = new TraceSource("PDFRenderer");

        private HandleRef handle;

        public PdfRenderer(string outputbase, string datadir)
        {
            DefaultPageSegMode = PageSegMode.Auto;

            IntPtr dataDirPtr = IntPtr.Zero;
            IntPtr outputBasePtr = IntPtr.Zero;

            dataDirPtr = MarshalHelper.StringToPtr(datadir, Encoding.UTF8);
            outputBasePtr = MarshalHelper.StringToPtr(outputbase, Encoding.UTF8);

            handle = new HandleRef(this, Interop.TessPdfRenderer.Native.TessPdfRendererCreate(outputBasePtr, dataDirPtr));
        }

        protected override void Dispose(bool disposing)
        {
            if (handle.Handle != IntPtr.Zero)
            {
                Interop.TessPdfRenderer.Native.TessDeletePdfRenderer(handle);
                handle = new HandleRef(this, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Begins the process of writing out a PDF document
        /// </summary>
        /// <param name="title">The title for the PDF</param>
        /// <returns></returns>
        public bool BeginDocument(string title)
        {
            IntPtr titlePtr = IntPtr.Zero;
            titlePtr = MarshalHelper.StringToPtr(title, Encoding.UTF8);

            int value = Interop.TessPdfRenderer.Native.TessPDFRendererBeginDocumentHandler(handle, titlePtr);
            bool ret = Convert.ToBoolean(value);

            return ret;
        }

        /// <summary>
        /// Ends the process of writing out the PDF and closes out the file
        /// </summary>
        /// <returns></returns>
        public bool EndDocumentHandler()
        {
            int value = Interop.TessPdfRenderer.Native.TessPDFRendererEndDocumentHandler(handle);
            bool ret = Convert.ToBoolean(value);

            return ret;
        }

        /// <summary>
        /// Gets or sets default <see cref="PageSegMode" /> mode used by <see cref="Tesseract.TesseractEngine.Process(Pix, Rect, PageSegMode?)" />.
        /// </summary>
        public PageSegMode DefaultPageSegMode
        {
            get;
            set;
        }

        /// <summary>
        /// The reference to the native DLL class instance
        /// </summary>
        internal HandleRef Handle
        {
            get { return handle; }
        }

    }
}

