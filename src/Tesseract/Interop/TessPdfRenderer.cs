using InteropDotNet;
using System;
using System.Runtime.InteropServices;

namespace Tesseract.Interop
{
    public interface ITessPdfRendererSignatures
    {
        /// <summary>
        /// Creates a new TessPdfRenderer instance
        /// </summary>
        /// <returns></returns>
        [RuntimeDllImport(Constants.TesseractDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TessPDFRendererCreate")]
        IntPtr TessPdfRendererCreate(IntPtr outputBasePtr, IntPtr dataDirPtr);

        /// <summary>
        /// Deletes a TessPdfRenderer instance.
        /// </summary>
        /// <returns></returns>
        [RuntimeDllImport(Constants.TesseractDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TessDeletePDFRenderer")]
        void TessDeletePdfRenderer(HandleRef ptr);

        /// <summary>
        /// Begins the process of writing out a PDF document. Writes PDF header to file
        /// </summary>
        /// <param name="handle">Handle of the native PDF renderer instance</param>
        /// <param name="titlePtr">Title for the PDF document</param>
        /// <returns></returns>
        [RuntimeDllImport(Constants.TesseractDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TessPDFRendererBeginDocument")]
        int TessPDFRendererBeginDocumentHandler(HandleRef handle, IntPtr titlePtr);

        /// <summary>
        /// Closes out the PDF file after writing footer information
        /// </summary>
        /// <param name="handle">Handle of the native PDF renderer instance</param>
        /// <returns></returns>
        [RuntimeDllImport(Constants.TesseractDllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TessPDFRendererEndDocument")]
        int TessPDFRendererEndDocumentHandler(HandleRef handle);

    }

    class TessPdfRenderer
    {
        private static ITessPdfRendererSignatures native;

        public static ITessPdfRendererSignatures Native
        {
            get
            {
                if (native == null)
                    Initialize();
                return native;
            }
        }

        public static void Initialize()
        {
            if (native == null)
            {
                LeptonicaApi.Initialize();
                native = InteropRuntimeImplementer.CreateInstance<ITessPdfRendererSignatures>();
            }
        }

    }
}

