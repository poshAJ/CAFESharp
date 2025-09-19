using System;
using System.Collections.Generic;
using UAssetAPI;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.Extensions;

public static class UAssetExtensions {
    public static string TryGetNameReferenceValue (
        this UAsset uasset,
        int index,
        Action<Exception>? onError
    ) {
        FString fString = uasset.GetNameReference(index: index);

        if (!fString.Value.StartsWith(value: "/Game/")) onError?.Invoke(new KeyNotFoundException());

        return fString.Value;
    }

    public static void TrySetNameReferenceValue (
        this UAsset uasset,
        int index,
        string value,
        Action<Exception>? onError
    ) {
        try {
            uasset.SetNameReference(
                index: index,
                value: (FString) value
            );
        } catch (Exception exception) {
            onError?.Invoke(exception);
        }
    }
}
