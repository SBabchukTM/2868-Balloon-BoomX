//
//  UniWebViewChannelMethodHandleRequest.cs
//  Created by Wang Wei(@onevcat) on 2023-04-21.
//
//  This file is a part of UniWebView Project (https://uniwebview.com)
//  By purchasing the asset, you are allowed to use this code in as many as projects 
//  you want, only if you publish the final products under the name of the same account
//  used for the purchase. 
//
//  This asset and all corresponding files (such as source code) are provided on an 
//  “as is” basis, without warranty of any kind, express of implied, including but not
//  limited to the warranties of merchantability, fitness for a particular purpose, and 
//  noninfringement. In no event shall the authors or copyright holders be liable for any 
//  claim, damages or other liability, whether in action of contract, tort or otherwise, 
//  arising from, out of or in connection with the software or the use of other dealing in the software.
//

using System;
using UnityEngine;

/// <summary>
/// Represents the request of a loading used in request handler.  
/// </summary>
[Serializable]
public class UniWebViewChannelMethodHandleRequest {
    [SerializeField]
    private string url;
    [SerializeField]
    private bool isMainFrame = true;

    /// <summary>
    /// The URL of the request.
    /// </summary>
    public string Url => url;
    /// <summary>
    /// Whether the request is for the main frame (top-level document) or an embedded frame (iframe).
    /// 
    /// Returns true for main frame requests, false for iframe requests.
    /// 
    /// Note: On older Android versions (API level < 24), this may always return true due to 
    /// platform limitations in distinguishing between main frame and iframe requests.
    /// </summary>
    public bool IsMainFrame => isMainFrame;
}