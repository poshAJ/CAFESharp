using System;
using UAssetAPI;
using UAssetAPI.UnrealTypes;

namespace CAFESharp.Extensions;

public static class UAssetExtensions {
    public static string TryGetNameReferenceValue (
        this UAsset uasset,
        int index,
        Action? onError
    ) {
        FString fString = uasset.GetNameReference(index: index);

        if (!fString.Value.StartsWith(value: "/Game/")) onError?.Invoke();

        return fString.Value;
    }

    public static void TrySetNameReferenceValue (
        this UAsset uasset,
        int index,
        string value,
        Action? onError
    ) {
        try {
            uasset.SetNameReference(
                index: index,
                value: (FString) value
            );
        } catch {
            onError?.Invoke();
        }
    }
}
