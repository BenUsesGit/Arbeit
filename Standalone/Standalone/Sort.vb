Imports System.Math
Public Class Sort

    ' Qicksort
    '------------------------------------------------------------------------------------------------------------------------------------------------------------ 
    Public Function QuickSort(ByVal ArrayToSort As Individuum(), ByVal ascending As Boolean)
        If ascending Then
            Return QuickSort(ArrayToSort, 0, ArrayToSort.Length - 1)
        Else
            ArrayToSort = QuickSort(ArrayToSort, 0, ArrayToSort.Length - 1)

            Dim arr_mem As Individuum()
            ReDim arr_mem(ArrayToSort.Length - 1)

            Dim i As Integer
            For i = 0 To arr_mem.Length - 1
                arr_mem(arr_mem.Length - 1 - i) = ArrayToSort(i)
            Next

            Return arr_mem
        End If

    End Function

    ' per default this function sorts an array from lowest to highest -- ascending
    Public Function QuickSort(ByVal ArrayToSort As Individuum(), ByVal Low As Long, ByVal High As Long)
        Dim vPartition As Individuum, vTemp As Individuum
        Dim i As Long, j As Long

        If Low > High Then Exit Function
        ' Rekursions-Abbruchbedingung 
        ' Ermittlung des Mittenelements zur Aufteilung in zwei Teilfelder: 
        vPartition = ArrayToSort(Round((Low + High) \ 2))
        ' Indizes i und j initial auf die äußeren Grenzen des Feldes setzen: 
        i = Low : j = High
        Do
            ' Von links nach rechts das linke Teilfeld durchsuchen: 
            Do While ArrayToSort(i).gFitness < vPartition.gFitness
                i = i + 1
            Loop
            ' Von rechts nach links das rechte Teilfeld durchsuchen: 
            Do While ArrayToSort(j).gFitness > vPartition.gFitness
                j = j - 1
            Loop
            If i <= j Then
                ' Die beiden gefundenen, falsch einsortierten Elemente austauschen:
                vTemp = ArrayToSort(j)
                ArrayToSort(j) = ArrayToSort(i)
                ArrayToSort(i) = vTemp
                i = i + 1
                j = j - 1
            End If
        Loop Until i > j
        ' Überschneidung der Indizes 
        ' Rekursive Sortierung der ausgewählten Teilfelder. Um die 
        ' Rekursionstiefe zu optimieren, wird (sofern die Teilfelder 
        ' nicht identisch groß sind) zuerst das kleinere 
        ' Teilfeld rekursiv sortiert. 
        If (j - Low) < (High - i) Then
            QuickSort(ArrayToSort, Low, j)
            QuickSort(ArrayToSort, i, High)
        Else
            QuickSort(ArrayToSort, i, High)
            QuickSort(ArrayToSort, Low, j)
        End If

        Return ArrayToSort
    End Function
    '------------------------------------------------------------------------------------------------------------------------------------------------------------
End Class
