using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SkillzSDK.Internal.API.Android
{
    class SkillzScoreCallback : AndroidJavaProxy
    {
        Action successCallback;
        Action<string> failureCallback;
        public SkillzScoreCallback(Action success, Action<string> failure)
            : base("com.skillz.SkillzScoreCallback")
        {
            successCallback = success;
            failureCallback = failure;
        }

        void success()
        {
			      if (successCallback != null)
			      {
                SkillzDebug.Log(SkillzDebug.Type.SIDEKICK, $"SkillzCrossPlatform.SubmitScore() Success Callback");
                successCallback();
			      }
        }

        void failure(AndroidJavaObject errorObj)
        {
            // errorObject is of type "Exception"
            if (failureCallback != null)
            {
                string errorMessage = errorObj.Call<string>("getMessage");
                SkillzDebug.Log(SkillzDebug.Type.SIDEKICK, $"SkillzCrossPlatform.SubmitScore() Failure Callback: {errorMessage}");
                failureCallback(errorMessage);
            }
        }
    }
}
