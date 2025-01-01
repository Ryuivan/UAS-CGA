using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildProcessor : IPreprocessBuildWithReport
{
    // Tentukan urutan eksekusi, semakin kecil angka, semakin awal dijalankan.
    public int callbackOrder => 0;

    // Fungsi yang dijalankan sebelum build dimulai
    public void OnPreprocessBuild(BuildReport report)
    {
        // Lokasi file high score
        string highScorePath = Path.Combine(Application.dataPath, "../highscore.txt");

        // Reset high score jika file ada
        if (File.Exists(highScorePath))
        {
            File.Delete(highScorePath);
            Debug.Log("High score file reset before build: " + highScorePath);
        }
        else
        {
            Debug.Log("No high score file found to reset.");
        }
    }
}
