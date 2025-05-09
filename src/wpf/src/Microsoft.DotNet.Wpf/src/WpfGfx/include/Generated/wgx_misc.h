// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


//---------------------------------------------------------------------------

//
// This file is automatically generated.  Please do not edit it directly.
//
// File name: wgx_misc.h
//---------------------------------------------------------------------------

#pragma once

//
// Some enums shouldn't be available in kernel mode, so we need to
// protect them by ifdef'ing them out.
//
#ifndef MILCORE_KERNEL_COMPONENT

//
// This determines how the colors in a gradient are interpolated.
//
BEGIN_MILENUM( MilColorInterpolationMode )
    //
    // Colors are interpolated in the scRGB color space
    //
    ScRgbLinearInterpolation = 0,

    //
    // Colors are interpolated in the sRGB color space
    //
    SRgbLinearInterpolation = 1,
END_MILENUM

//
// Enum which describes whether certain values should be considered as absolute 
// local coordinates or whether they should be considered multiples of a bounding 
// box's size.
//
BEGIN_MILENUM( MilBrushMappingMode )
    //
    // Absolute means that the values in question will be interpreted directly in 
    // local space.
    //
    Absolute = 0,

    //
    // RelativeToBoundingBox means that the values will be interpreted as a multiples 
    // of a bounding box, where 1.0 is considered 100% of the bounding box measure.
    //
    RelativeToBoundingBox = 1,
END_MILENUM

//
// This determines how a gradient fills the space outside its primary area.
//
BEGIN_MILENUM( MilGradientSpreadMethod )
    //
    // Pad - The final color in the gradient is used to fill the remaining area.
    //
    Pad = 0,

    //
    // Reflect - The gradient is mirrored and repeated, then mirrored again, etc.
    //
    Reflect = 1,

    //
    // Repeat - The gradient is drawn again and again.
    //
    Repeat = 2,
END_MILENUM

//
// Enum which descibes how a source rect should be stretched to fit a destination 
// rect.
//
BEGIN_MILENUM( MilStretch )
    //
    // Preserve original size
    //
    None = 0,

    //
    // Aspect ratio is not preserved, source rect fills destination rect.
    //
    Fill = 1,

    //
    // Aspect ratio is preserved, Source rect is uniformly scaled as large as possible 
    // such that both width and height fit within destination rect.  This will not 
    // cause source clipping, but it may result in unfilled areas of the destination 
    // rect, if the aspect ratio of source and destination are different.
    //
    Uniform = 2,

    //
    // Aspect ratio is preserved, Source rect is uniformly scaled as small as possible 
    // such that the entire destination rect is filled.  This can cause source 
    // clipping, if the aspect ratio of source and destination are different.
    //
    UniformToFill = 3,
END_MILENUM

//
// Flags determining the transparency of a render target.
//
BEGIN_MILFLAGENUM( MilTransparency )
    //
    // Default is opaque.
    //
    Opaque = 0,

    //
    // Constant alpha.
    //
    ConstantAlpha = 1,

    //
    // Per pixel alpha.
    //
    PerPixelAlpha = 2,

    //
    // Color key.
    //
    ColorKey = 4,
END_MILFLAGENUM

//
// Enum determining the window layer type.
//
BEGIN_MILENUM( MilWindowLayerType )
    //
    // Not layered.
    //
    NotLayered = 0,

    //
    // System managed layer.
    //
    SystemManagedLayer = 1,

    //
    // Application managed layer
    //
    ApplicationManagedLayer = 2,
END_MILENUM

//
// Enum determining the caching mode for the hosted window target.
//
BEGIN_MILENUM( MilWindowTargetCachingMode )
    //
    // Not cached, visuals are directly connected.
    //
    NotCached = 0,

    //
    // Cached, single buffered.
    //
    Cached = 1,
END_MILENUM

//
// Enum which descibes the drawing of the ends of a line.
//
BEGIN_MILENUM( MilTileMode )
    //
    // Do not tile only the base tile is drawn, the remaining area is left as 
    // transparent
    //
    None = 0,

    //
    // The basic tile mode  the base tile is drawn and the remaining area is filled by 
    // repeating the base tile such that the right edge of one tile is adjacent to the 
    // left edge of the next, and similarly for bottom and top
    //
    Tile = 4,

    //
    // The same as tile, but alternate columns of tiles are flipped horizontally.  The 
    // base tile is drawn untransformed.
    //
    FlipX = 1,

    //
    // The same as tile, but alternate rows of tiles are flipped vertically.  The base 
    // tile is drawn untransformed.
    //
    FlipY = 2,

    //
    // The combination of FlipX and FlipY.  The base tile is drawn untransformed.
    //
    FlipXY = 3,

    //
    // Extend the edges of the tile out indefinitely
    //
    Extend = 5,
END_MILENUM

//
// The AlignmentX enum is used to describe how content is positioned horizontally 
// within a container.
//
BEGIN_MILENUM( MilHorizontalAlignment )
    //
    // Align contents towards the left of a space.
    //
    Left = 0,

    //
    // Center contents horizontally.
    //
    Center = 1,

    //
    // Align contents towards the right of a space.
    //
    Right = 2,
END_MILENUM

//
// The AlignmentY enum is used to describe how content is positioned vertically 
// within a container.
//
BEGIN_MILENUM( MilVerticalAlignment )
    //
    // Align contents towards the top of a space.
    //
    Top = 0,

    //
    // Center contents vertically.
    //
    Center = 1,

    //
    // Align contents towards the bottom of a space.
    //
    Bottom = 2,
END_MILENUM

//
// Enum which descibes the drawing of the ends of a line.
//
BEGIN_MILENUM( MilPenCap )
    //
    // Flat line cap.
    //
    Flat = 0,

    //
    // Square line cap.
    //
    Square = 1,

    //
    // Round line cap.
    //
    Round = 2,

    //
    // Triangle line cap.
    //
    Triangle = 3,
END_MILENUM

//
// Enum which descibes the drawing of the corners on the line.
//
BEGIN_MILENUM( MilPenJoin )
    //
    // Miter join.
    //
    Miter = 0,

    //
    // Bevel join.
    //
    Bevel = 1,

    //
    // Round join.
    //
    Round = 2,
END_MILENUM

//
// This enumeration describes the type of combine operation to be performed.
//
BEGIN_MILENUM( MilCombineMode )
    //
    // Produce a geometry representing the set of points contained in either
    // the first or the second geometry.
    //
    Union = 0,

    //
    // Produce a geometry representing the set of points common to the first
    // and the second geometries.
    //
    Intersect = 1,

    //
    // Produce a geometry representing the set of points contained in the
    // first geometry or the second geometry, but not both.
    //
    Xor = 2,

    //
    // Produce a geometry representing the set of points contained in the
    // first geometry but not the second geometry.
    //
    Exclude = 3,
END_MILENUM

//
// Enum which descibes the manner in which we render edges of non-text primitives.
//
BEGIN_MILENUM( MilEdgeMode )
    //
    // No edge mode specfied - do not alter the current edge mode applied to this 
    // content.
    //
    Unspecified = 0,

    //
    // Render edges of non-text primitives as aliased edges.
    //
    Aliased = 1,

    Last,
END_MILENUM

//
// Enum which describes the manner in which we scale the images.
//
BEGIN_MILENUM( MilBitmapScalingMode )
    //
    // Rendering engine will chose the optimal algorithm
    //
    Unspecified = 0,

    //
    // Rendering engine will use the fastest mode to scale the images. This may mean a 
    // low quality image
    //
    LowQuality = 1,

    //
    // Rendering engine will use the mode which produces a most quality image
    //
    HighQuality = 2,

    //
    // Rendering engine will use linear interpolation.
    //
    Linear = 1,

    //
    // Rendering engine will use fant interpolation.
    //
    Fant = 2,

    //
    // Rendering engine will use nearest-neighbor interpolation.
    //
    NearestNeighbor = 3,

    Last,
END_MILENUM

//
// Enum used for hinting the rendering engine that text can be rendered with 
// ClearType.
//
BEGIN_MILENUM( MilClearTypeHint )
    //
    // Rendering engine will use ClearType when it is determined possible.  If an 
    // intermediate render target has been introduced in the ancestor tree, ClearType 
    // will be disabled.
    //
    Auto = 0,

    //
    // Rendering engine will enable ClearType for this element subtree.  Where an 
    // intermediate render target is introduced in this subtree, ClearType will once 
    // again be disabled.
    //
    Enabled = 1,

    Last,
END_MILENUM

//
// Enum used for hinting the rendering engine that rendered content can be cached
//
BEGIN_MILENUM( MilCachingHint )
    //
    // Rendering engine will choose algorithm.
    //
    Unspecified = 0,

    //
    // Cache rendered content when possible.
    //
    Cache = 1,

    Last,
END_MILENUM

//
// Enum used for specifying what filter mode text should be rendered with 
// (ClearType, grayscale, aliased).
//
BEGIN_MILENUM( MilTextRenderingMode )
    //
    // Rendering engine will use a rendering mode compatible with the 
    // TextFormattingMode specified for the control
    //
    Auto = 0,

    //
    // Rendering engine will render text with aliased filtering when possible
    //
    Aliased = 1,

    //
    // Rendering engine will render text with grayscale filtering when possible
    //
    Grayscale = 2,

    //
    // Rendering engine will render text with ClearType filtering when possible
    //
    ClearType = 3,

    Last,
END_MILENUM

//
// Enum used for specifying how text should be rendered with respect to animated 
// or static text
//
BEGIN_MILENUM( MilTextHintingMode )
    //
    // Rendering engine will automatically determine whether to draw text with quality 
    // settings appropriate to animated or static text
    //
    Auto = 0,

    //
    // Rendering engine will render text for highest static quality
    //
    Fixed = 1,

    //
    // Rendering engine will render text for highest animated quality
    //
    Animated = 2,

    Last,
END_MILENUM

//
// Type of blur kernel to use.
//
BEGIN_MILENUM( MilKernelType )
    //
    // Use a Guassian filter
    //
    Gaussian = 0,

    //
    // Use a Box filter
    //
    Box = 1,
END_MILENUM

//
// Type of edge profile to use.
//
BEGIN_MILENUM( MilEdgeProfile )
    //
    // Use a Linear edge profile
    //
    Linear = 0,

    //
    // Use a curved in edge profile
    //
    CurvedIn = 1,

    //
    // Use a curved out edge profile
    //
    CurvedOut = 2,

    //
    // Use a bulged up edge profile
    //
    BulgedUp = 3,
END_MILENUM

//
// Policy for rendering the shader in software.
//
BEGIN_MILENUM( ShaderEffectShaderRenderMode )
    //
    // Allow hardware and software
    //
    Auto = 0,

    //
    // Force software rendering
    //
    SoftwareOnly = 1,

    //
    // Require hardware rendering, ignore otherwise
    //
    HardwareOnly = 2,
END_MILENUM

//
// Type of bias to give rendering of the effect
//
BEGIN_MILENUM( MilEffectRenderingBias )
    //
    // Bias towards performance
    //
    Performance = 0,

    //
    // Bias towards quality
    //
    Quality = 1,
END_MILENUM

BEGIN_MILFLAGENUM( MilGlyphRun )
    //
    // Exposed flags: these values are used in third party rasterizers.
    //

    Sideways = 0x00000001,


    //
    // Internal flags:
    //

    HasOffsets = 0x00000010,
END_MILFLAGENUM

BEGIN_MILENUM( MilMessageClass )
    //
    // invalid message
    //
    Invalid = 0x00,


    //
    // messages
    //

    SyncFlushReply = 0x01,
    Tier = 0x04,
    CompositionDeviceStateChange = 0x05,
    PartitionIsZombie = 0x06,
    SyncModeStatus = 0x09,
    Presented = 0x0A,
    RenderStatus = 0x0E,
    BadPixelShader = 0x10,


    //
    // Not a real message. This value is one more than message with the greatest 
    // numerical value.
    //
    Last,
END_MILENUM

//
// This enumeration determines the type of the segment.
//
BEGIN_MILENUM( MilSegmentType )
    //
    // The segment is invalid. This enumeration value SHOULD never be used.
    //
    None = 0,

    //
    // The segment is a line segment.
    //
    Line = 1,

    //
    // The segment is a cubic Bezier segment.
    //
    Bezier = 2,

    //
    // The segment is a quadratic Bezier segment.
    //
    QuadraticBezier = 3,

    //
    // The segment is an elliptical arc segment.
    //
    Arc = 4,

    //
    // This segment is a series of line segments.
    //
    PolyLine = 5,

    //
    // This segment is a series of cubic Bezier segments.
    //
    PolyBezier = 6,

    //
    // This segment is a series of quadratic Bezier segments.
    //
    PolyQuadraticBezier = 7,
END_MILENUM

//
// This enumeration defines flags of the segment.
//
BEGIN_MILFLAGENUM( MilCoreSeg )
    TypeLine = 0x00000001,
    TypeBezier = 0x00000002,
    TypeMask = 0x00000003,

    //
    // When this bit is set then this segment is not to be stroked
    //
    IsAGap = 0x00000004,

    //
    // When this bit is set then the join between this segment and the PREVIOUS
    // segment will be rounded upon widening, regardless of the pen line join
    // property.
    //
    SmoothJoin = 0x00000008,

    //
    // When this bit is set on the first type then the figure should be
    // closed.
    //
    Closed = 0x00000010,

    //
    // This bit indicates whether the segment is curved.
    //
    IsCurved = 0x00000020,
END_MILFLAGENUM

//
// This enumeration specifies the render target initialization flags. These
// flags can be combined using the bit-wise alternative operation to describe
// more complex properties.
//
BEGIN_MILFLAGENUM( MilRTInitialization )
    //
    // Default initialization flags (0) imply hardware with software fallback,
    // synchronized to reduce tearing for hw RTs, and no retention of contents
    // between scenes.
    //
    Default = 0x00000000,

    //
    // This flag disables the hardware accelerated RT. Use only software.
    //
    SoftwareOnly = 0x00000001,

    //
    // This flag disables the software RT. Use only hardware.
    //
    HardwareOnly = 0x00000002,

    //
    // Creates a dummy render target that consumes all calls
    //
    Null = 0x00000003,

    //
    // Mask for choice of render target
    //
    TypeMask = 0x00000003,

    //
    // This flag indicates that presentation should not wait for any specific
    // time to promote the results to the display. This may result in display
    // tearing.
    //
    PresentImmediately = 0x00000004,

    //
    // This flag makes the RT reatin the contents from one frame to the next.
    // Retaining the contents has performance implications.  For scene changes
    // with little to update retaining contents may help, but if most of the
    // scene will be repainted anyway, retention may hurt some hw scenarios.
    //
    PresentRetainContents = 0x00000008,

    //
    // This flag indicates that the render target backbuffer will have
    // an alpha channel that is at least 8 bits wide.
    //
    NeedDestinationAlpha = 0x00000040,

    //
    // This flag assumes that all resources (such as bitmaps and render
    // targets) are released on the same thread as the rendering device.  This
    // flag enables us to use a single threaded dx device instead of a
    // multi-threaded one.
    //
    SingleThreadedUsage = 0x00000100,

    //
    // This flag directs the render target to extend its presentation area
    // to include the non-client area.  The origin of the render target space
    // will be equal to the origin of the window.
    //
    RenderNonClient = 0x00000200,

    //
    // This flag directed the render target not to restrict its rendering and
    // presentation to the visible portion of window on the desktop.  This is
    // useful for when the window position may be faked or the system may try
    // to make use of window contents that are not recognized as visible.  For
    // example DWM thumbnails expect a fully rendered and presented window.
    // Note: This does not guarantee that some clipping will not be used.
    //
    DisableDisplayClipping = 0x00001000,

    //
    // This flag forces the creation of a render target bitmap to match its
    // parent's type, so a software surface only creates software RTs and a
    // hardware surface only creates hardware RTs.  This is necessary for the
    // hardware-accelerated bitmap effects pipeline to guarantee that we do
    // not encounter a situation where we're trying to run shaders sampling
    // from a hardware texture to render into a software intermediate.
    //
    ForceCompatible = 0x00002000,

    //
    // This flag is the same as DisableDisplayClipping except that it disables
    // display clipping on multi-monitor configurations in all OS'. This flag is 
    // automatically
    // set on Windows 8 and newer systems. If WPF decides to unset
    // DisableDisplayClipping, then DisableMultimonDisplayClipping flag will not be
    // respected even if set by an applicaiton via its manifest
    //
    DisableMultimonDisplayClipping = 0x00004000,

    //
    // This flag is passed down by PresentationCore to tell wpfgfx that
    // the DisableMultimonDisplayClipping compatibity flag is set by the user. This
    // allows us to distinguish between when DisableMultimonDisplayClipping == 0 means
    // that the user set it to false explicitly, versus when the user didn't set it
    // and the DisableMultimonDisplayClipping bit happens to be implicitly set to 0
    //
    IsDisableMultimonDisplayClippingValid = 0x00008000,

    //
    // This flag directs the render target to render the full scene,
    // bypassing D3D's dirty-rectangle optimizations.
    //
    DisableDirtyRectangles = 0x00010000,


    //
    // Test only / internal flags
    //


    //
    // This flag forces the render target to use the d3d9 reference raster
    // when using d3d. (Should be combined with Default or HardwareOnly)
    // This is designed for test apps only
    //
    UseRefRast = 0x01000000,

    //
    // This flag forces the render target to use the rgb reference raster
    // when using d3d.( Should be combined with Default or HardwareOnly )
    // This is designed for test apps only
    //
    UseRgbRast = 0x02000000,


    //
    // We support 4 primary present modes:
    // 1) Present using D3D
    // 2) Present using BitBlt to a DC
    // 3) Present using AlphaBlend to a DC
    // 4) Present using UpdateLayeredWindow
    //

    PresentUsingMask = 0xC0000000,
    PresentUsingHal = 0x00000000,
    PresentUsingBitBlt = 0x40000000,
    PresentUsingAlphaBlend = 0x80000000,
    PresentUsingUpdateLayeredWindow = 0xC0000000,
END_MILFLAGENUM

BEGIN_MILENUM( MilPresentationResults )
    VSync,
    NoPresent,
    VSyncUnsupported,
    Dwm,
END_MILENUM

BEGIN_MILFLAGENUM( MilRenderOptionFlags )
    BitmapScalingMode = 0x00000001,
    EdgeMode = 0x00000002,
    CompositingMode = 0x00000004,
    ClearTypeHint = 0x00000008,
    TextRenderingMode = 0x00000010,
    TextHintingMode = 0x00000020,
    Last,
END_MILFLAGENUM

BEGIN_MILENUM( MilBitmapWrapMode )
    Extend = 0,
    FlipX = 1,
    FlipY = 2,
    FlipXY = 3,
    Tile = 4,
    Border = 5,
END_MILENUM

BEGIN_MILFLAGENUM( MilWindowProperties )
    //
    // WS_EX_LAYOUTRTL
    //
    RtlLayout = 0x0001,

    Redirected = 0x0002,

    //
    // WS_EX_COMPOSITED
    //
    Compositited = 0x0004,

    //
    // Present this window using GDI
    //
    PresentUsingGdi = 0x0008,
END_MILFLAGENUM

BEGIN_MILFLAGENUM( MilPathGeometryFlags )
    HasCurves = 0x00000001,
    BoundsValid = 0x00000002,
    HasGaps = 0x00000004,
    HasHollows = 0x00000008,
    IsRegionData = 0x00000010,
    Mask = 0x0000001F,
END_MILFLAGENUM

BEGIN_MILFLAGENUM( MilPathFigureFlags )
    HasGaps = 0x00000001,
    HasCurves = 0x00000002,
    IsClosed = 0x00000004,
    IsFillable = 0x00000008,
    IsRectangleData = 0x00000010,
    Mask = 0x0000001F,
END_MILFLAGENUM

BEGIN_MILENUM( MilDashStyle )
    Solid = 0,
    Dash = 1,
    Dot = 2,
    DashDot = 3,
    DashDotDot = 4,
    Custom = 5,
END_MILENUM

BEGIN_MILENUM( MilFillMode )
    Alternate = 0,
    Winding = 1,
END_MILENUM

BEGIN_MILENUM( MilGradientWrapMode )
    Extend = 0,
    Flip = 1,
    Tile = 2,
END_MILENUM

//
// This enumeration describes how a source rectangle should be stretched to fit
// a destination rectangle.
//
BEGIN_MILENUM( MilStretchMode )
    None = 0,
    Fill = 1,
    Uniform = 2,
    UniformToFill = 3,
END_MILENUM

BEGIN_MILENUM( MilCompositingMode )
    SourceOver = 0,
    SourceCopy = 1,
    SourceAdd = 2,
    SourceAlphaMultiply = 3,
    SourceInverseAlphaMultiply = 4,
    SourceUnder = 5,

    //
    // Do not use the non-premultiplied blend with premultiplied
    // sources.  Use non-premultiplied sources carefully.
    //
    SourceOverNonPremultiplied = 6,

    SourceInverseAlphaOverNonPremultiplied = 7,
    DestInvert = 8,
    Last,
END_MILENUM

BEGIN_MILENUM( MilCompositionDeviceState )
    Normal = 0,
    NoDevice = 1,
    Occluded = 2,
    Last,
END_MILENUM

//
// MIL marshal type (related to the transport type)
//
BEGIN_MILENUM( MilMarshalType )
    Invalid = 0x0,
    SameThread,
    CrossThread,
END_MILENUM

#endif // MILCORE_KERNEL_COMPONENT


#ifndef _MilMatrix3x2D_DEFINED

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMatrix3x2D
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE S_11;
    DOUBLE S_12;
    DOUBLE S_21;
    DOUBLE S_22;
    DOUBLE DX;
    DOUBLE DY;
} MilMatrix3x2D;

#define _MilMatrix3x2D_DEFINED

#endif // _MilMatrix3x2D_DEFINED

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPoint2F
//
//------------------------------------------------------------------------------
typedef struct
{
    FLOAT X;
    FLOAT Y;
} MilPoint2F;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilColorI
//
//------------------------------------------------------------------------------
typedef struct
{
    INT r;
    INT g;
    INT b;
    INT a;
} MilColorI;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPoint3F
//
//------------------------------------------------------------------------------
typedef struct
{
    FLOAT X;
    FLOAT Y;
    FLOAT Z;
} MilPoint3F;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilQuaternionF
//
//------------------------------------------------------------------------------
typedef struct
{
    FLOAT X;
    FLOAT Y;
    FLOAT Z;
    FLOAT W;
} MilQuaternionF;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMatrix4x4D
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE M_11;
    DOUBLE M_12;
    DOUBLE M_13;
    DOUBLE M_14;
    DOUBLE M_21;
    DOUBLE M_22;
    DOUBLE M_23;
    DOUBLE M_24;
    DOUBLE M_31;
    DOUBLE M_32;
    DOUBLE M_33;
    DOUBLE M_34;
    DOUBLE M_41;
    DOUBLE M_42;
    DOUBLE M_43;
    DOUBLE M_44;
} MilMatrix4x4D;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilGraphicsAccelerationCaps
//
//  Synopsis:
//      Description of a display or display set's graphics capabilities.
//
//------------------------------------------------------------------------------
typedef struct
{
    //
    // Tier value
    //
    INT TierValue;

    //
    // True if WDDM driver is supporting display
    //
    INT HasWDDMSupport;

    //
    // pixel shader version
    //
    UINT PixelShaderVersion;

    //
    // vertex shader version
    //
    UINT VertexShaderVersion;

    //
    // max texture width
    //
    UINT MaxTextureWidth;

    //
    // max texture height
    //
    UINT MaxTextureHeight;

    //
    // The accelerated rendering is supported for a windowed application
    //
    INT WindowCompatibleMode;

    //
    // Per pixel bit depth of display
    //
    UINT BitsPerPixel;

    //
    // Processor support for SSE2 instruction set.
    //
    UINT HasSSE2Support;

    //
    // Maximum number of instruction slots, if pixel shader 3.0 is supported
    //
    UINT MaxPixelShader30InstructionSlots;
} MilGraphicsAccelerationCaps;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilGraphicsAccelerationAssessment
//
//  Synopsis:
//      Assessment of the video memory bandwidth and total video memory as set by 
//      WinSAT. Used by the DWM to determine glass and opaque glass capability of the 
//      display machine.
//
//------------------------------------------------------------------------------
typedef struct
{
    UINT VideoMemoryBandwidth;
    UINT VideoMemorySize;
} MilGraphicsAccelerationAssessment;


//
// Some structs shouldn't be available in kernel mode, so we need to
// protect them by ifdef'ing them out.
//
#ifndef MILCORE_KERNEL_COMPONENT

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPoint2L
//
//------------------------------------------------------------------------------
typedef struct
{
    INT X;
    INT Y;
} MilPoint2L;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPoint2D
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE X;
    DOUBLE Y;
} MilPoint2D;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPointAndSizeL
//
//------------------------------------------------------------------------------
typedef struct
{
    INT X;
    INT Y;
    INT Width;
    INT Height;
} MilPointAndSizeL;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPointAndSizeF
//
//------------------------------------------------------------------------------
typedef struct
{
    FLOAT X;
    FLOAT Y;
    FLOAT Width;
    FLOAT Height;
} MilPointAndSizeF;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilRectF
//
//------------------------------------------------------------------------------
typedef struct
{
    FLOAT left;
    FLOAT top;
    FLOAT right;
    FLOAT bottom;
} MilRectF;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPointAndSizeD
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE X;
    DOUBLE Y;
    DOUBLE Width;
    DOUBLE Height;
} MilPointAndSizeD;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilRectD
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE left;
    DOUBLE top;
    DOUBLE right;
    DOUBLE bottom;
} MilRectD;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSizeD
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE Width;
    DOUBLE Height;
} MilSizeD;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilGradientStop
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE Position;
    MilColorF Color;
} MilGradientStop;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPathGeometry
//
//------------------------------------------------------------------------------
typedef struct
{
    DWORD Size;
    DWORD Flags;
    MilRectD Bounds;
    UINT FigureCount;
    DWORD ForcePacking;
} MilPathGeometry;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPathFigure
//
//------------------------------------------------------------------------------
typedef struct
{
    DWORD BackSize;
    DWORD Flags;
    UINT Count;
    UINT Size;
    MilPoint2D StartPoint;
    UINT OffsetToLastSegment;

    //
    // See ForcePacking comment at beginning of this file.
    //
    UINT ForcePacking;
} MilPathFigure;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegment
//
//------------------------------------------------------------------------------
typedef struct
{
    MilSegmentType::Enum Type;
    DWORD Flags;
    DWORD BackSize;
} MilSegment;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegmentLine
//
//------------------------------------------------------------------------------
typedef struct : MilSegment
{
    //
    // See ForcePacking comment at beginning of this file.
    //
    UINT ForcePacking;

    MilPoint2D Point;
} MilSegmentLine;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegmentBezier
//
//------------------------------------------------------------------------------
typedef struct : MilSegment
{
    //
    // See ForcePacking comment at beginning of this file.
    //
    UINT ForcePacking;

    MilPoint2D Point1;
    MilPoint2D Point2;
    MilPoint2D Point3;
} MilSegmentBezier;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegmentQuadraticBezier
//
//------------------------------------------------------------------------------
typedef struct : MilSegment
{
    //
    // See ForcePacking comment at beginning of this file.
    //
    UINT ForcePacking;

    MilPoint2D Point1;
    MilPoint2D Point2;
} MilSegmentQuadraticBezier;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegmentArc
//
//------------------------------------------------------------------------------
typedef struct : MilSegment
{
    UINT LargeArc;
    MilPoint2D Point;
    MilSizeD Size;
    DOUBLE XRotation;
    UINT Sweep;

    //
    // See ForcePacking comment at beginning of this file.
    //
    UINT ForcePacking;
} MilSegmentArc;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilSegmentPoly
//
//------------------------------------------------------------------------------
typedef struct : MilSegment
{
    UINT Count;
} MilSegmentPoly;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilPenData
//
//------------------------------------------------------------------------------
typedef struct
{
    DOUBLE Thickness;
    DOUBLE MiterLimit;
    DOUBLE DashOffset;
    MilPenCap::Enum StartLineCap;
    MilPenCap::Enum EndLineCap;
    MilPenCap::Enum DashCap;
    MilPenJoin::Enum LineJoin;
    UINT DashArraySize;
} MilPenData;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilRenderOptions
//
//------------------------------------------------------------------------------
typedef struct
{
    MilRenderOptionFlags::Flags Flags;
    MilEdgeMode::Enum EdgeMode;
    MilCompositingMode::Enum CompositingMode;
    MilBitmapScalingMode::Enum BitmapScalingMode;
    MilClearTypeHint::Enum ClearTypeHint;
    MilTextRenderingMode::Enum TextRenderingMode;
    MilTextHintingMode::Enum TextHintingMode;
} MilRenderOptions;

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgCompositionDeviceStateChangeData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    MilCompositionDeviceState::Enum deviceStateOld;
    MilCompositionDeviceState::Enum deviceStateNew;
} MilMsgCompositionDeviceStateChangeData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgSyncFlushReplyData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    HRESULT hr;
} MilMsgSyncFlushReplyData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgVersionReplyData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    UINT SupportedVersionsCount;
} MilMsgVersionReplyData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgTierData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    //
    // Is this caps description specific to the primary display or is it the minimum 
    // common
    // value across all the displays?
    //
    UINT CommonMinimumCaps;

    //
    // Display uniqueness signature. These caps are only valid for given signature.
    //
    UINT DisplayUniqueness;

    MilGraphicsAccelerationCaps Caps;
    MilGraphicsAccelerationAssessment Assessment;
} MilMsgTierData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgPartitionIsZombieData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    HRESULT hrFailureCode;
} MilMsgPartitionIsZombieData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgSyncModeStatusData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    HRESULT hrEnabled;
} MilMsgSyncModeStatusData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgPresentedData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    MilPresentationResults::Enum presentationResults;
    UINT refreshRate;
    LARGE_INTEGER presentationTime;
} MilMsgPresentedData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgSysMemUsageData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    UINT percentSystemMemoryUsed;
    size_t totalClientSystemMemory;
} MilMsgSysMemUsageData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgAsyncFlushReplyData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    UINT responseToken;
    HRESULT hrCode;
} MilMsgAsyncFlushReplyData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MilMsgRenderStatusData
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    HRESULT hrCode;
} MilMsgRenderStatusData;
#pragma pack(pop)

//+-----------------------------------------------------------------------------
//
//  Class:
//      MIL_MESSAGE
//
//------------------------------------------------------------------------------
#pragma pack(push, 1)
typedef struct
{
    MilMessageClass::Enum type;
    DWORD dwReserved;

    union
    {
        MilMsgSyncFlushReplyData syncFlushReplyData;
        MilMsgTierData tierData;
        MilMsgPartitionIsZombieData partitionIsZombieData;
        MilMsgCompositionDeviceStateChangeData deviceStateChangeData;
        MilMsgSyncModeStatusData syncModeStatusData;
        MilMsgPresentedData presentationTimeData;
        MilMsgSysMemUsageData systemMemoryUsageData;
        MilMsgAsyncFlushReplyData asyncFlushData;
        MilMsgRenderStatusData renderStatusData;
    };
} MIL_MESSAGE;
#pragma pack(pop)

#endif // MILCORE_KERNEL_COMPONENT
