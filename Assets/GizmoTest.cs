using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTest : MonoBehaviour
{
    public float radius = 1.0f;         // Радиус сферы
    public float torusRadius = 0.1f;    // Радиус каждого маленького круга (толщина тора)
    public int longitudeLines = 36;     // Количество долгот (вертикальные линии)
    public int latitudeLines = 18;      // Количество широт (горизонтальные кольца)
    public int torusSegments = 12;      // Количество сегментов в каждом маленьком торе
    public Color gizmoColor = Color.white; // Цвет гизмо

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        DrawTorusSphere(transform.position, radius, torusRadius, longitudeLines, latitudeLines, torusSegments);
    }

    private void DrawTorusSphere(Vector3 center, float radius, float torusRadius, int longitude, int latitude, int torusSeg)
    {
        float longitudeStep = 360f / longitude; // Шаг по долготе
        float latitudeStep = 180f / latitude;   // Шаг по широте

        float radianLongitudeStep = Mathf.Deg2Rad * longitudeStep;
        float radianLatitudeStep = Mathf.Deg2Rad * latitudeStep;

        // === 1. Рисуем торовые окружности вдоль широт ===
        for (int lat = 1; lat < latitude; lat++) // Не рисуем на полюсах
        {
            float latAngle = -90f + lat * latitudeStep; // Угол от -90 до 90 градусов
            float radLat = Mathf.Deg2Rad * latAngle;

            float ringRadius = Mathf.Cos(radLat) * radius; // Радиус окружности на текущей широте
            float y = Mathf.Sin(radLat) * radius; // Высота круга

            Vector3 ringCenter = center + new Vector3(0, y, 0); // Центр окружности широты

            for (int lon = 0; lon < longitude; lon++)
            {
                float lonAngle = lon * longitudeStep;
                float radLon = Mathf.Deg2Rad * lonAngle;

                // Центр маленького тора на данной широте
                Vector3 torusCenter = center + new Vector3(
                    Mathf.Cos(radLon) * ringRadius,
                    y,
                    Mathf.Sin(radLon) * ringRadius
                );

                // Рисуем маленький круг (тор) вокруг этого центра
                DrawCircle(torusCenter, torusRadius, torusSeg, Quaternion.LookRotation(torusCenter - ringCenter));
            }
        }

        // === 2. Рисуем меридианы (вертикальные линии) ===
        for (int lon = 0; lon < longitude; lon++)
        {
            float lonAngle = lon * longitudeStep;
            float radLon = Mathf.Deg2Rad * lonAngle;

            Vector3 prevPoint = center + new Vector3(
                Mathf.Cos(radLon) * radius,
                -radius,
                Mathf.Sin(radLon) * radius
            );

            for (int lat = 1; lat <= latitude; lat++)
            {
                float latAngle = -90f + lat * latitudeStep;
                float radLat = Mathf.Deg2Rad * latAngle;

                Vector3 newPoint = center + new Vector3(
                    Mathf.Cos(radLon) * Mathf.Cos(radLat) * radius,
                    Mathf.Sin(radLat) * radius,
                    Mathf.Sin(radLon) * Mathf.Cos(radLat) * radius
                );

                Gizmos.DrawLine(prevPoint, newPoint);
                prevPoint = newPoint;
            }
        }
    }

    private void DrawCircle(Vector3 center, float radius, int segments, Quaternion rotation)
    {
        float angleStep = 360f / segments;
        float radianStep = Mathf.Deg2Rad * angleStep;

        Vector3 prevPoint = center + rotation * new Vector3(radius, 0, 0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * radianStep;
            Vector3 newPoint = center + rotation * new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            Gizmos.DrawLine(prevPoint, newPoint);
            prevPoint = newPoint;
        }
    }
}
