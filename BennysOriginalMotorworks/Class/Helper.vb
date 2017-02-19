﻿Imports System.Runtime.InteropServices
Imports GTA
Imports GTA.Math
Imports GTA.Native

Public Class Helper

    Public Shared Function CreateVehicle(VehicleModel As String, VehicleHash As Integer, Position As Vector3, Optional Heading As Single = 0) As Vehicle
        Dim Result As Vehicle = Nothing
        If VehicleModel = "" Then
            Dim model = New Model(VehicleHash)
            model.Request(250)
            If model.IsInCdImage AndAlso model.IsValid Then
                While Not model.IsLoaded
                    Script.Wait(50)
                End While
                Result = WorldCreateVehicle(model, Position, Heading)
            End If
            model.MarkAsNoLongerNeeded()
        Else
            Dim model = New Model(VehicleModel)
            model.Request(250)
            If model.IsInCdImage AndAlso model.IsValid Then
                While Not model.IsLoaded
                    Script.Wait(50)
                End While
                Result = WorldCreateVehicle(model, Position, Heading)
            End If
            model.MarkAsNoLongerNeeded()
        End If
        Return Result
    End Function

    Public Shared Function WorldCreateVehicle(model As Model, position As Vector3, Optional heading As Single = 0F) As Vehicle
        If Not model.IsVehicle OrElse Not model.Request(1000) Then
            Return Nothing
        End If

        Return New Vehicle(Native.Function.Call(Of Integer)(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading,
        False, False))
    End Function

    Public Shared Sub LoadMPDLCMap()
        Native.Function.Call(Hash._LOAD_MP_DLC_MAPS)
        LoadMPDLCMapMissingObjects()
    End Sub

    Public Shared Sub LoadMPDLCMapMissingObjects()
        Dim TID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -1155.31005, -1518.5699, 10.6300001) 'Floyd Apartment
        Dim MID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -802.31097, 175.05599, 72.84459) 'Michael House
        Dim FID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -9.96562, -1438.54003, 31.101499) 'Franklin Aunt House
        Dim FID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 0.91675, 528.48498, 174.628005) 'Franklin House

        Dim WODID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -172.983001, 494.032989, 137.654006) '3655 Wild Oats
        Dim NCAID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 340.941009, 437.17999, 149.389999) '2044 North Conker
        Dim NCAID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 373.0230102, 416.1050109, 145.70100402) '2045 North Conker
        Dim HCAID1 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -676.1270141, 588.6119995, 145.16999816) '2862 Hillcrest Avenue
        Dim HCAID2 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -763.10699462, 615.90600585, 144.139999) '2868 Hillcrest Avenue
        Dim HCAID3 As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -857.79797363, 682.56298828, 152.6529998) '2874 Hillcrest Avenue
        Dim MRID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -572.60998535, 653.13000488, 145.63000488) '2117 Milton Road
        Dim WMDID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, 120.5, 549.952026367, 184.09700012207) '3677 Whispymound Drive
        Dim MWTDID As Integer = Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, -1288, 440.74798583, 97.694602966) '2113 Mad Wayne Thunder Drive

        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID1, "V_57_FranklinStuff")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_clean_apt")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "layer_whiskey")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "layer_sextoys_a")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_mrJam_A")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, TID2, "swap_sofa_A")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_bed_tidy")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_L_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_S_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_D_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_M_Items")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "Michael_premier")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MID, "V_Michael_plane_ticket")

        'Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "showhome_only")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "franklin_settled")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "franklin_unpacking")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "bong_and_wine")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_flyer")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_tshirt")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "progress_tux")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, FID2, "unlocked")

        Native.Function.Call(Hash._0x55E86AF2712B36A1, WODID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, NCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, NCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID1, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID2, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, HCAID3, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MRID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, WMDID, "Stilts_Kitchen_Window")
        Native.Function.Call(Hash._0x55E86AF2712B36A1, MWTDID, "Stilts_Kitchen_Window")

        Native.Function.Call(Hash.REFRESH_INTERIOR, FID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, TID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, FID2)

        Native.Function.Call(Hash.REFRESH_INTERIOR, WODID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, NCAID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, NCAID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID1)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID2)
        Native.Function.Call(Hash.REFRESH_INTERIOR, HCAID3)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MRID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, WMDID)
        Native.Function.Call(Hash.REFRESH_INTERIOR, MWTDID)
    End Sub

    Public Shared Sub DisplayHelpTextThisFrame(helpText As String, Optional Shape As Integer = -1)
        Native.Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "CELL_EMAIL_BCON")
        Const maxStringLength As Integer = 99

        Dim i As Integer = 0
        While i < helpText.Length
            Native.Function.Call(Hash._0x6C188BE134E074AA, helpText.Substring(i, System.Math.Min(maxStringLength, helpText.Length - i)))
            i += maxStringLength
        End While
        Native.Function.Call(Hash._DISPLAY_HELP_TEXT_FROM_STRING_LABEL, 0, 0, 1, Shape)
    End Sub

    Public Shared Function GetInteriorID(interior As Vector3) As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_INTERIOR_AT_COORDS, interior.X, interior.Y, interior.Z)
    End Function

    Public Shared Function LowriderUpgrade(model As Model) As Model
        Dim result As Model = model
        Select Case model
            Case "banshee"
                result = "banshee2"
            Case "buccaneer"
                result = "buccaneer2"
            Case "chino"
                result = "chino2"
            Case "diabolus"
                result = "diabolus2"
            Case "comet2"
                result = "comet3"
            Case "faction"
                result = "faction2"
            Case "faction2"
                result = "faction3"
            Case "fcr"
                result = "fcr2"
            Case "italigtb"
                result = "italigtb2"
            Case "minivan"
                result = "minivan2"
            Case "moonbeam"
                result = "moonbeam"
            Case "nero"
                result = "nero2"
            Case "primo"
                result = "primo2"
            Case "sabregt"
                result = "sabregt2"
            Case "slamvan"
                result = "slamvan3"
            Case "specter"
                result = "specter2"
            Case "sultan"
                result = "sultanrs"
            Case "tornado", "tornado2", "tornado3"
                result = "tornado5"
            Case "virgo3"
                result = "virgo2"
            Case "voodoo2"
                result = "voodoo"
            Case "elegy2"
                result = "elegy"
            Case Else
                result = model
        End Select
        Return result
    End Function

    Public Enum ScreenEffect
        SwitchHudIn
        SwitchHudOut
        FocusIn
        FocusOut
        MinigameEndNeutral
        MinigameEndTrevor
        MinigameEndFranklin
        MinigameEndMichael
        MinigameTransitionOut
        MinigameTransitionIn
        SwitchShortNeutralIn
        SwitchShortFranklinIn
        SwitchShortTrevorIn
        SwitchShortMichaelIn
        SwitchOpenMichaelIn
        SwitchOpenFranklinIn
        SwitchOpenTrevorIn
        SwitchHudMichaelOut
        SwitchHudFranklinOut
        SwitchHudTrevorOut
        SwitchShortFranklinMid
        SwitchShortMichaelMid
        SwitchShortTrevorMid
        DeathFailOut
        CamPushInNeutral
        CamPushInFranklin
        CamPushInMichael
        CamPushInTrevor
        SwitchSceneFranklin
        SwitchSceneTrevor
        SwitchSceneMichael
        SwitchSceneNeutral
        MpCelebWin
        MpCelebWinOut
        MpCelebLose
        MpCelebLoseOut
        DeathFailNeutralIn
        DeathFailMpDark
        DeathFailMpIn
        MpCelebPreloadFade
        PeyoteEndOut
        PeyoteEndIn
        PeyoteIn
        PeyoteOut
        MpRaceCrash
        SuccessFranklin
        SuccessTrevor
        SuccessMichael
        DrugsMichaelAliensFightIn
        DrugsMichaelAliensFight
        DrugsMichaelAliensFightOut
        DrugsTrevorClownsFightIn
        DrugsTrevorClownsFight
        DrugsTrevorClownsFightOut
        HeistCelebPass
        HeistCelebPassBw
        HeistCelebEnd
        HeistCelebToast
        MenuMgHeistIn
        MenuMgTournamentIn
        MenuMgSelectionIn
        ChopVision
        DmtFlightIntro
        DmtFlight
        DrugsDrivingIn
        DrugsDrivingOut
        SwitchOpenNeutralFib5
        HeistLocate
        MpJobLoad
        RaceTurbo
        MpIntroLogo
        HeistTripSkipFade
        MenuMgHeistOut
        MpCoronaSwitch
        MenuMgSelectionTint
        SuccessNeutral
        ExplosionJosh3
        SniperOverlay
        RampageOut
        Rampage
        DontTazemeBro
    End Enum

    Public Shared Sub ScreenEffectStart(effectName As ScreenEffect, Optional duration As Integer = 0, Optional looped As Boolean = False)
        Native.Function.Call(Hash._START_SCREEN_EFFECT, New InputArgument() {[Enum].GetName(GetType(ScreenEffect), effectName), duration, looped})
    End Sub

    Public Shared Function GetAccentColor(vehicle As Vehicle) As VehicleColor
        Dim arg As New OutputArgument()
        Native.Function.Call(&HB7635E80A5C31BFFUL, vehicle, arg)
        Return arg.GetResult(Of VehicleColor)()
    End Function

    Public Shared Sub SetAccentColor(vehicle As Vehicle, color As VehicleColor)
        Native.Function.Call(&H6089CDF6A57F326C, vehicle, color)
    End Sub

    Public Shared Function LocalizedModTypeName(toggleModType As VehicleToggleMod, Optional stock As Boolean = False) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim result As String = Nothing
        If stock = True Then
            result = Game.GetGXTEntry("CMOD_ARM_0")
        Else
            result = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, Bennys.veh, toggleModType)
            If result = "" Then
                'would only happen if the text isnt loaded
                result = [Enum].GetName(GetType(VehicleToggleMod), toggleModType)
            End If
        End If
        Return result
    End Function

    Public Shared Function LocalizedModTypeName(modType As VehicleMod) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String = Nothing
        Select Case modType
            Case VehicleMod.Armor
                cur = Game.GetGXTEntry("CMOD_MOD_ARM")
                Exit Select
            Case VehicleMod.Brakes
                cur = Game.GetGXTEntry("CMOD_MOD_BRA")
                Exit Select
            Case VehicleMod.Engine
                cur = Game.GetGXTEntry("CMOD_MOD_ENG")
                Exit Select
            Case VehicleMod.Suspension
                cur = Game.GetGXTEntry("CMOD_MOD_SUS")
                Exit Select
            Case VehicleMod.Transmission
                cur = Game.GetGXTEntry("CMOD_MOD_TRN")
                Exit Select
            Case VehicleMod.Horns
                cur = Game.GetGXTEntry("CMOD_MOD_HRN")
                Exit Select
            Case VehicleMod.FrontWheels
                If Not Bennys.veh.Model.IsBike AndAlso Bennys.veh.Model.IsBicycle Then
                    cur = Game.GetGXTEntry("CMOD_MOD_WHEM")
                    If cur = "" Then
                        Return "Wheels"
                    End If
                Else
                    cur = Game.GetGXTEntry("CMOD_WHE0_0")
                End If
                Exit Select
            Case VehicleMod.BackWheels
                cur = Game.GetGXTEntry("CMOD_WHE0_1")
                Exit Select

            'Bennys
            Case VehicleMod.PlateHolder
                cur = Game.GetGXTEntry("CMM_MOD_S0")
                Exit Select
            Case VehicleMod.VanityPlates
                cur = Game.GetGXTEntry("CMM_MOD_S1")
                Exit Select
            Case VehicleMod.TrimDesign
                If Bennys.veh.Model = VehicleHash.SultanRS Then
                    cur = Game.GetGXTEntry("CMM_MOD_S2b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S2")
                End If
                Exit Select
            Case VehicleMod.Ornaments
                cur = Game.GetGXTEntry("CMM_MOD_S3")
                Exit Select
            Case VehicleMod.Dashboard
                cur = Game.GetGXTEntry("CMM_MOD_S4")
                Exit Select
            Case VehicleMod.DialDesign
                cur = Game.GetGXTEntry("CMM_MOD_S5")
                Exit Select
            Case VehicleMod.DoorSpeakers
                cur = Game.GetGXTEntry("CMM_MOD_S6")
                Exit Select
            Case VehicleMod.Seats
                cur = Game.GetGXTEntry("CMM_MOD_S7")
                Exit Select
            Case VehicleMod.SteeringWheels
                cur = Game.GetGXTEntry("CMM_MOD_S8")
                Exit Select
            Case VehicleMod.ColumnShifterLevers
                cur = Game.GetGXTEntry("CMM_MOD_S9")
                Exit Select
            Case VehicleMod.Plaques
                cur = Game.GetGXTEntry("CMM_MOD_S10")
                Exit Select
            Case VehicleMod.Speakers
                cur = Game.GetGXTEntry("CMM_MOD_S11")
                Exit Select
            Case VehicleMod.Trunk
                cur = Game.GetGXTEntry("CMM_MOD_S12")
                Exit Select
            Case VehicleMod.Hydraulics
                cur = Game.GetGXTEntry("CMM_MOD_S13")
                Exit Select
            Case VehicleMod.EngineBlock
                cur = Game.GetGXTEntry("CMM_MOD_S14")
                Exit Select
            Case VehicleMod.AirFilter
                If Bennys.veh.Model = VehicleHash.SultanRS Then
                    cur = Game.GetGXTEntry("CMM_MOD_S15b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S15")
                End If
                Exit Select
            Case VehicleMod.Struts
                If Bennys.veh.Model = VehicleHash.SultanRS OrElse Bennys.veh.Model = VehicleHash.Banshee2 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S16b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S16")
                End If
                Exit Select
            Case VehicleMod.ArchCover
                If Bennys.veh.Model = VehicleHash.SultanRS Then
                    cur = Game.GetGXTEntry("CMM_MOD_S17b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S17")
                End If
                Exit Select
            Case VehicleMod.Aerials
                If Bennys.veh.Model = VehicleHash.SultanRS Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18b")
                ElseIf Bennys.veh.Model = VehicleHash.BType3 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S18c")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S18")
                End If
                Exit Select
            Case VehicleMod.Trim
                If Bennys.veh.Model = VehicleHash.SultanRS Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19b")
                ElseIf Bennys.veh.Model = VehicleHash.BType3 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19c")
                ElseIf Bennys.veh.Model = VehicleHash.Virgo2 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S19d")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S19")
                End If
                Exit Select
            Case VehicleMod.Tank
                If Bennys.veh.Model = VehicleHash.SlamVan3 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S27")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S20")
                End If
                Exit Select

            Case VehicleMod.Windows
                If Bennys.veh.Model = VehicleHash.BType3 Then
                    cur = Game.GetGXTEntry("CMM_MOD_S21b")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S21")
                End If
                Exit Select
            Case DirectCast(47, VehicleMod)
                If Bennys.veh.Model = VehicleHash.SlamVan3 Then
                    cur = Game.GetGXTEntry("SLVAN3_RDOOR")
                Else
                    cur = Game.GetGXTEntry("CMM_MOD_S22")
                End If
                Exit Select
            Case VehicleMod.Livery
                cur = Game.GetGXTEntry("CMM_MOD_S23")
                Exit Select
            Case Else

                cur = Native.Function.Call(Of String)(Hash.GET_MOD_SLOT_NAME, Bennys.veh.Handle, modType)
                If DoesGXTEntryExist(cur) Then
                    cur = Game.GetGXTEntry(cur)
                End If
                Exit Select
        End Select
        If cur = "" Then
            'would only happen if the text isnt loaded
            cur = [Enum].GetName(GetType(VehicleMod), modType)
        End If

        Return cur
    End Function

    Public Shared Function DoesGXTEntryExist(entry As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.DOES_TEXT_LABEL_EXIST, entry)
    End Function

    Public Shared Function GetLocalizedModName(index As Integer, modCount As Integer, modType As VehicleMod) As String
        'this still needs a little more work, but its better than what it used to be
        If modCount = 0 Then
            Return ""
        End If
        If index < -1 OrElse index >= modCount Then
            Return ""
        End If
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        Dim cur As String
        If modType = VehicleMod.Horns Then
            If _hornNames.ContainsKey(index) Then
                If DoesGXTEntryExist(_hornNames(index).Item1) Then
                    Return Game.GetGXTEntry(_hornNames(index).Item1)
                End If
                Return _hornNames(index).Item2
            End If
            Return ""
        End If
        If modType = VehicleMod.FrontWheels OrElse modType = VehicleMod.BackWheels Then
            If index = -1 Then
                If Not Bennys.veh.Model.IsBike AndAlso Bennys.veh.Model.IsBicycle Then
                    Return Game.GetGXTEntry("CMOD_WHE_0")
                Else
                    Return Game.GetGXTEntry("CMOD_WHE_B_0")
                End If
            End If
            If index >= modCount / 2 Then
                Return Game.GetGXTEntry("CHROME") + " " + Game.GetGXTEntry(Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index))
            Else
                Return Game.GetGXTEntry(Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index))
            End If
        End If

        Select Case modType
            Case VehicleMod.Armor
                Return Game.GetGXTEntry("CMOD_ARM_" + (index + 1).ToString())
            Case VehicleMod.Brakes
                Return Game.GetGXTEntry("CMOD_BRA_" + (index + 1).ToString())
            Case VehicleMod.Engine
                If index = -1 Then
                    'Engine doesn't list anything in LSC for no parts, but there is a setting with no part. so just use armours none
                    Return Game.GetGXTEntry("CMOD_ARM_0")
                End If
                Return Game.GetGXTEntry("CMOD_ENG_" + (index + 2).ToString())
            Case VehicleMod.Suspension
                Return Game.GetGXTEntry("CMOD_SUS_" + (index + 1).ToString())
            Case VehicleMod.Transmission
                Return Game.GetGXTEntry("CMOD_GBX_" + (index + 1).ToString())
        End Select
        If index > -1 Then
            cur = Native.Function.Call(Of String)(Hash.GET_MOD_TEXT_LABEL, Bennys.veh.Handle, modType, index)
            If DoesGXTEntryExist(cur) Then
                cur = Game.GetGXTEntry(cur)
                If cur = "" OrElse cur = "NULL" Then
                    Return LocalizedModTypeName(modType) + " " + (index + 1).ToString()
                End If
                Return cur
            End If
            Return LocalizedModTypeName(modType) + " " + (index + 1).ToString()
        Else
            Select Case modType
                Case VehicleMod.AirFilter
                    If Bennys.veh.Model = VehicleHash.Tornado Then
                    End If
                    Exit Select
                Case VehicleMod.Struts
                    Select Case Bennys.veh.Model
                        Case VehicleHash.Banshee, VehicleHash.Banshee2, VehicleHash.SultanRS
                            Return Game.GetGXTEntry("CMOD_COL5_41")
                    End Select
                    Exit Select

            End Select
            Return Game.GetGXTEntry("CMOD_DEF_0")
        End If
    End Function

    Public Shared Function GetLocalizedWheelTypeName(wheelType As VehicleWheelType) As String
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, "mod_mnu", 10) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 10, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, "mod_mnu", 10)
        End If
        If _wheelNames.ContainsKey(wheelType) Then
            If DoesGXTEntryExist(_wheelNames(wheelType).Item1) Then
                Return Game.GetGXTEntry(_wheelNames(wheelType).Item1)
            End If
            Return _wheelNames(wheelType).Item2
        End If
        Throw New ArgumentException("Wheel Type is undefined", "wheelType")
    End Function

    Private Shared ReadOnly _hornNames As New Dictionary(Of Integer, Tuple(Of String, String))(New Dictionary(Of Integer, Tuple(Of String, String))() From {
    {-1, New Tuple(Of String, String)("CMOD_HRN_0", "Stock Horn")},
    {0, New Tuple(Of String, String)("CMOD_HRN_TRK", "Truck Horn")},
    {1, New Tuple(Of String, String)("CMOD_HRN_COP", "Cop Horn")},
    {2, New Tuple(Of String, String)("CMOD_HRN_CLO", "Clown Horn")},
    {3, New Tuple(Of String, String)("CMOD_HRN_MUS1", "Musical Horn 1")},
    {4, New Tuple(Of String, String)("CMOD_HRN_MUS2", "Musical Horn 2")},
    {5, New Tuple(Of String, String)("CMOD_HRN_MUS3", "Musical Horn 3")},
    {6, New Tuple(Of String, String)("CMOD_HRN_MUS4", "Musical Horn 4")},
    {7, New Tuple(Of String, String)("CMOD_HRN_MUS5", "Musical Horn 5")},
    {8, New Tuple(Of String, String)("CMOD_HRN_SAD", "Sad Trombone")},
    {9, New Tuple(Of String, String)("HORN_CLAS1", "Classical Horn 1")},
    {10, New Tuple(Of String, String)("HORN_CLAS2", "Classical Horn 2")},
    {11, New Tuple(Of String, String)("HORN_CLAS3", "Classical Horn 3")},
    {12, New Tuple(Of String, String)("HORN_CLAS4", "Classical Horn 4")},
    {13, New Tuple(Of String, String)("HORN_CLAS5", "Classical Horn 5")},
    {14, New Tuple(Of String, String)("HORN_CLAS6", "Classical Horn 6")},
    {15, New Tuple(Of String, String)("HORN_CLAS7", "Classical Horn 7")},
    {16, New Tuple(Of String, String)("HORN_CNOTE_C0", "Scale Do")},
    {17, New Tuple(Of String, String)("HORN_CNOTE_D0", "Scale Re")},
    {18, New Tuple(Of String, String)("HORN_CNOTE_E0", "Scale Mi")},
    {19, New Tuple(Of String, String)("HORN_CNOTE_F0", "Scale Fa")},
    {20, New Tuple(Of String, String)("HORN_CNOTE_G0", "Scale Sol")},
    {21, New Tuple(Of String, String)("HORN_CNOTE_A0", "Scale La")},
    {22, New Tuple(Of String, String)("HORN_CNOTE_B0", "Scale Ti")},
    {23, New Tuple(Of String, String)("HORN_CNOTE_C1", "Scale Do (High)")},
    {24, New Tuple(Of String, String)("HORN_HIPS1", "Jazz Horn 1")},
    {25, New Tuple(Of String, String)("HORN_HIPS2", "Jazz Horn 2")},
    {26, New Tuple(Of String, String)("HORN_HIPS3", "Jazz Horn 3")},
    {27, New Tuple(Of String, String)("HORN_HIPS4", "Jazz Horn Loop")},
    {28, New Tuple(Of String, String)("HORN_INDI_1", "Star Spangled Banner 1")},
    {29, New Tuple(Of String, String)("HORN_INDI_2", "Star Spangled Banner 2")},
    {30, New Tuple(Of String, String)("HORN_INDI_3", "Star Spangled Banner 3")},
    {31, New Tuple(Of String, String)("HORN_INDI_4", "Star Spangled Banner 4")},
    {32, New Tuple(Of String, String)("HORN_LUXE2", "Classical Horn Loop 1")},
    {33, New Tuple(Of String, String)("HORN_LUXE1", "Classical Horn 8")},
    {34, New Tuple(Of String, String)("HORN_LUXE3", "Classical Horn Loop 2")},
    {35, New Tuple(Of String, String)("HORN_LUXE2", "Classical Horn Loop 1")},
    {36, New Tuple(Of String, String)("HORN_LUXE1", "Classical Horn 8")},
    {37, New Tuple(Of String, String)("HORN_LUXE3", "Classical Horn Loop 2")},
    {38, New Tuple(Of String, String)("HORN_HWEEN1", "Halloween Loop 1")},
    {39, New Tuple(Of String, String)("HORN_HWEEN1", "Halloween Loop 1")},
    {40, New Tuple(Of String, String)("HORN_HWEEN2", "Halloween Loop 2")},
    {41, New Tuple(Of String, String)("HORN_HWEEN2", "Halloween Loop 2")},
    {42, New Tuple(Of String, String)("HORN_LOWRDER1", "San Andreas Loop")},
    {43, New Tuple(Of String, String)("HORN_LOWRDER1", "San Andreas Loop")},
    {44, New Tuple(Of String, String)("HORN_LOWRDER2", "Liberty City Loop")},
    {45, New Tuple(Of String, String)("HORN_LOWRDER2", "Liberty City Loop")},
    {46, New Tuple(Of String, String)("HORN_XM15_1", "Festive Loop 1")},
    {47, New Tuple(Of String, String)("HORN_XM15_2", "Festive Loop 2")},
    {48, New Tuple(Of String, String)("HORN_XM15_3", "Festive Loop 3")}
})

    Private Shared ReadOnly _wheelNames As New Dictionary(Of VehicleWheelType, Tuple(Of String, String))(New Dictionary(Of VehicleWheelType, Tuple(Of String, String))() From {
    {VehicleWheelType.BikeWheels, New Tuple(Of String, String)("CMOD_WHE1_0", "Bike")},
    {VehicleWheelType.HighEnd, New Tuple(Of String, String)("CMOD_WHE1_1", "High End")},
    {VehicleWheelType.Lowrider, New Tuple(Of String, String)("CMOD_WHE1_2", "Lowrider")},
    {VehicleWheelType.Muscle, New Tuple(Of String, String)("CMOD_WHE1_3", "Muscle")},
    {VehicleWheelType.Offroad, New Tuple(Of String, String)("CMOD_WHE1_4", "Offroad")},
    {VehicleWheelType.Sport, New Tuple(Of String, String)("CMOD_WHE1_5", "Sport")},
    {VehicleWheelType.SUV, New Tuple(Of String, String)("CMOD_WHE1_6", "SUV")},
    {VehicleWheelType.Tuner, New Tuple(Of String, String)("CMOD_WHE1_7", "Tuner")},
    {8, New Tuple(Of String, String)("CMOD_WHE1_8", "Benny's Originals")},
    {9, New Tuple(Of String, String)("CMOD_WHE1_9", "Benny's Bespoke")}
})

    Public Shared Function IsCustomWheels() As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.GET_VEHICLE_MOD_VARIATION, Bennys.veh, VehicleMod.FrontWheels)
    End Function

    Enum NeonLayouts
        None
        Sides = 3
        Front
        FrontAndSides = 7
        Back
        BackAndSides = 11
        FrontAndBack
        FrontBackAndSides = 15
    End Enum

    Public Shared Function NeonLayout() As NeonLayouts
        Dim v As Vehicle = Bennys.veh
        Dim back As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Back)
        Dim front As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Front)
        Dim left As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Left)
        Dim right As Boolean = v.IsNeonLightsOn(VehicleNeonLight.Right)
        Dim result As Integer = -1

        If Not back AndAlso Not front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.None
        ElseIf Not back AndAlso front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.Front
        ElseIf back AndAlso Not front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.Back
        ElseIf Not back AndAlso Not front AndAlso left AndAlso right Then
            result = NeonLayouts.Sides
        ElseIf back AndAlso front AndAlso Not left AndAlso Not right Then
            result = NeonLayouts.FrontAndBack
        ElseIf Not back AndAlso front AndAlso left AndAlso right Then
            result = NeonLayouts.FrontAndSides
        ElseIf back AndAlso Not front AndAlso left AndAlso right Then
            result = NeonLayouts.BackAndSides
        ElseIf back AndAlso front AndAlso left AndAlso right Then
            result = NeonLayouts.FrontBackAndSides
        End If

        Return result
    End Function
End Class