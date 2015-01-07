//----------------------------------------------------------------------------
//  Copyright (C) 2004-2014 by EMGU Corporation. All rights reserved.       
//----------------------------------------------------------------------------

using Emgu.CV.CvEnum;
using UnityEngine;
using System;
using System.Drawing;
using System.Collections;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Runtime.InteropServices;

namespace Emgu.CV
{
   public static class TextureConvert
   {
      public static Image<TColor, TDepth> Texture2dToImage<TColor, TDepth>(Texture2D texture, bool correctForVerticleFlip)
         where TColor : struct, IColor
         where TDepth : new()
      {
         int width = texture.width;
         int height = texture.height;

         Image<TColor, TDepth> result = new Image<TColor, TDepth>(width, height);
         try
         {
            Color32[] colors = texture.GetPixels32();
            GCHandle handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            using (Image<Rgba, Byte> rgba = new Image<Rgba, byte>(width, height, width * 4, handle.AddrOfPinnedObject()))
            {
               result.ConvertFrom(rgba);
            }
            handle.Free();
         }
         catch (Exception)
         {
         //   byte[] jpgBytes = texture.EncodeToJPG();
         //   using (Mat tmp = new Mat())
         //   {
         //      CvInvoke.cvDecodeImage(jpgBytes, LOAD_IMAGE_TYPE.CV_LOAD_IMAGE_ANYCOLOR, tmp);
         //      result.ConvertFrom(tmp);
         //   }
             throw;
         }
         if (correctForVerticleFlip)
            CvInvoke.cvFlip(result, result, FLIP.VERTICAL);
         return result;
         
         

      }

      public static Texture2D ImageToTexture2D<TColor, TDepth>(Image<TColor, TDepth> image, bool correctForVerticleFlip)
         where TColor : struct, IColor
         where TDepth : new()
      {
         Size size = image.Size;

         if (typeof(TColor) == typeof(Rgb) && typeof(TDepth) == typeof(Byte))
         {
            Texture2D texture = new Texture2D(size.Width, size.Height, TextureFormat.RGB24, false);
            byte[] data = new byte[size.Width * size.Height * 3];
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            using (Image<Rgb, byte> rgb = new Image<Rgb, byte>(size.Width, size.Height, size.Width * 3, dataHandle.AddrOfPinnedObject()))
            {
               rgb.ConvertFrom(image);
               if (correctForVerticleFlip)
                  CvInvoke.cvFlip(rgb, rgb, FLIP.VERTICAL);
            }
            dataHandle.Free();
            texture.LoadRawTextureData(data);
            texture.Apply();
            return texture;
         }
         else //if (typeof(TColor) == typeof(Rgba) && typeof(TDepth) == typeof(Byte))
         {
            Texture2D texture = new Texture2D(size.Width, size.Height, TextureFormat.RGBA32, false);
            byte[] data = new byte[size.Width * size.Height * 4];
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            using (Image<Rgba, byte> rgba = new Image<Rgba, byte>(size.Width, size.Height, size.Width * 4, dataHandle.AddrOfPinnedObject()))
            {
               rgba.ConvertFrom(image);
               if (correctForVerticleFlip)
                  CvInvoke.cvFlip(rgba, rgba, FLIP.VERTICAL);
            }
            dataHandle.Free();
            texture.LoadRawTextureData(data);

            texture.Apply();
            return texture;
         }

         //return null;
      }

      /*
      /// <summary>
      /// 
      /// </summary>
      /// <param name="image">The input image, if 3 channel, we assume it is Bgr, if 4 channels, we assume it is Bgra</param>
      /// <param name="correctForVerticleFlip"></param>
      /// <returns></returns>
      public static Texture2D ToTexture2D(IInputArray image, bool correctForVerticleFlip = true)
      {
         using (Mat m = image.GetInputArray().GetMat())
         {
            Size size = m.Size;

            if (m.NumberOfChannels == 3 && m.Depth == DepthType.Cv8U)
            {
               Texture2D texture = new Texture2D(size.Width, size.Height, TextureFormat.RGB24, false);
               byte[] data = new byte[size.Width*size.Height*3];
               GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
               using (
                  Image<Rgb, byte> rgb = new Image<Rgb, byte>(size.Width, size.Height, size.Width*3,
                     dataHandle.AddrOfPinnedObject()))
               {
                  rgb.ConvertFrom(m);
                  if (correctForVerticleFlip)
                     CvInvoke.Flip(rgb, rgb, Emgu.CV.CvEnum.FlipType.Vertical);
               }
               dataHandle.Free();
               texture.LoadRawTextureData(data);
               texture.Apply();
               return texture;
            }
            else //if (typeof(TColor) == typeof(Rgba) && typeof(TDepth) == typeof(Byte))
            {
               Texture2D texture = new Texture2D(size.Width, size.Height, TextureFormat.RGBA32, false);
               byte[] data = new byte[size.Width*size.Height*4];
               GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
               using (
                  Image<Rgba, byte> rgba = new Image<Rgba, byte>(size.Width, size.Height, size.Width*4,
                     dataHandle.AddrOfPinnedObject()))
               {
                  rgba.ConvertFrom(m);
                  if (correctForVerticleFlip)
                     CvInvoke.Flip(rgba, rgba, Emgu.CV.CvEnum.FlipType.Vertical);
               }
               dataHandle.Free();
               texture.LoadRawTextureData(data);

               texture.Apply();
               return texture;
            }
         }
      }*/
   }
}