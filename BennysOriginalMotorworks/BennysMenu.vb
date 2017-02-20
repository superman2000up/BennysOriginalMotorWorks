﻿Imports INMNativeUI
Imports GTA
Imports GTA.Native
Imports GTA.Math

Public Class BennysMenu
    Inherits Script

    Public Shared lowriders As List(Of Model) = New List(Of Model) From {"banshee", "Buccaneer", "chino", "diabolus", "comet2", "faction", "faction2", "fcr", "italigtb", "minivan", "moonbeam", "nero", "primo", "sabregt",
        "slamvan", "specter", "sultan", "tornado", "tornado2", "tornado3", "virgo3", "voodoo2", "elegy2"}
    Public Shared tyres As String() = New String() {"Stock", "Thin White", "White", "Fat White", "Red", "Blue", "Atomic"}
    Public Shared MainMenu, gmBodywork, gmEngine, gmInterior, gmPlate, gmLights, gmRespray, gmWheels, gmBumper, gmWheelType, gmNeonKits, gmDoor As UIMenu
    Public Shared mAerials, mSuspension, mArmor, mBrakes, mEngine, mTransmission, mFBumper, mRBumper, mSSkirt, mTrim, mEngineBlock, mAirFilter, mStruts, mColumnShifterLevers, mDashboard, mDialDesign, mOrnaments, mSeats,
        mSteeringWheels, mTrimDesign, mPlateHolder, mVanityPlates, mNumberPlate, mBikeWheels, mHighEnd, mLowrider, mMuscle, mOffroad, mSport, mSUV, mTuner, mBennysOriginals, mBespoke, mTires, mHeadlights, mNeon, mNeonColor,
    mArchCover, mExhaust, mFender, mRFender, mDoor, mFrame, mGrille, mHood, mHorn, mHydraulics, mLivery, mPlaques, mRoof, mSpeakers, mSpoilers, mTank, mTrunk, mWindow, mTurbo, mTint As UIMenu
    Public Shared iRepair, iHorn, iArmor, iBrakes, iFBumper, iExhaust, iFender, iRollcage, iRoof, iTransmission, iEngine, iPlate, iLights, iTint, iTurbo, iRespray, iWheels, iSuspension, iEngineBlock, iAerials, iAirFilter,
        iArchCover, iDoor, iFrame, iGrille, iHood, iHydraulics, iLivery, iPlaques, iRFender, iSpeaker, iSpoilers, iTank, iTrunk, iWindows, iTrim, iUpgrade, iStruts, iTrimColor, iColumnShifterLevers, iDashboard, iDialDesign,
        iOrnaments, iSeats, iSteeringWheels, iTrimDesign, iRBumper, iSideSkirt, iRimColor, iPlateHolder, iVanityPlates, iHeadlights, iDashboardColor, iNumberPlate, iBikeWheels, iHighEnd, iLowrider, iMuscle, iOffroad,
    iSport, iSUV, iTuner, iBennys, iBespoke, iTires, iNeon, iTireSmoke, iNeonColor As UIMenuItem
    Public Shared giBodywork, giEngine, giInterior, giPlate, giLights, giRespray, giWheels, giBumper, giWheelType, giTires, giNeonKits, giPrimaryCol, giSecondaryCol, giAccentCol, giBikeWheels, giHighEndWheels, giDoor,
        giLowriderWheels, giMuscleWheels, giOffroadWheels, giSportWheels, giSUVWheels, giTunerWheels, giBennysWheels, giBespokeWheels, giFBumper, giRBumper, giSSkirt, giNumberPlate, giVanityPlate, giPlateHolder As UIMenuItem
    Public Shared _menuPool As MenuPool
    Public Shared camera As WorkshopCamera

    Public Shared Sub CreateMainMenu()
        Try
            MainMenu = New UIMenu("", "MAIN MENU")
            MainMenu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            MainMenu.MouseEdgeEnabled = False
            _menuPool.Add(MainMenu)
            MainMenu.AddItem(New UIMenuItem("Noting"))
            MainMenu.RefreshIndex()
            AddHandler MainMenu.OnMenuClose, AddressOf MainMenuCloseHandler
            AddHandler MainMenu.OnItemSelect, AddressOf MainMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshMainMenu()
        Try
            MainMenu.MenuItems.Clear()

            If Bennys.veh.IsDamaged Then
                iRepair = New UIMenuItem("Repair", Game.GetGXTEntry("CMOD_MOD_0_D"))
                MainMenu.AddItem(iRepair)
            Else
                'Specials
                If lowriders.Contains(Bennys.veh.Model) Then
                    iUpgrade = New UIMenuItem("Upgrade")
                    MainMenu.AddItem(iUpgrade)
                End If

                'Groups
                If (Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Trim) <> 0) Then
                    giBodywork = New UIMenuItem("Bodywork")
                    MainMenu.AddItem(giBodywork)
                    MainMenu.BindMenuToItem(gmBodywork, giBodywork)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Or Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Or Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Struts) <> 0) Then
                    giEngine = New UIMenuItem("Engine")
                    MainMenu.AddItem(giEngine)
                    MainMenu.BindMenuToItem(gmEngine, giEngine)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Or Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Or Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0) Then
                    giInterior = New UIMenuItem("Interior")
                    MainMenu.AddItem(giInterior)
                    MainMenu.BindMenuToItem(gmInterior, giInterior)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Or Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0) Then
                    giBumper = New UIMenuItem("Bumper")
                    MainMenu.AddItem(giBumper)
                    MainMenu.BindMenuToItem(gmBumper, giBumper)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Or Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0) Then
                    giPlate = New UIMenuItem("Plate")
                    MainMenu.AddItem(giPlate)
                    MainMenu.BindMenuToItem(gmPlate, giPlate)
                End If
                If (Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 Or Bennys.veh.GetModCount(VehicleMod.Windows) <> 0) Then
                    giDoor = New UIMenuItem("Door")
                    MainMenu.AddItem(giDoor)
                    MainMenu.BindMenuToItem(gmDoor, giDoor)
                End If
                giWheels = New UIMenuItem("Wheels")
                MainMenu.AddItem(giWheels)
                MainMenu.BindMenuToItem(gmWheels, giWheels)
                giLights = New UIMenuItem("Lights")
                MainMenu.AddItem(giLights)
                MainMenu.BindMenuToItem(gmLights, giLights)
                giRespray = New UIMenuItem("Respray")
                MainMenu.AddItem(giRespray)
                MainMenu.BindMenuToItem(gmRespray, giRespray)

                'Single Item
                If Bennys.veh.GetModCount(VehicleMod.ArchCover) <> 0 Then
                    iArchCover = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.ArchCover)) 'Arch Covers
                    MainMenu.AddItem(iArchCover)
                    MainMenu.BindMenuToItem(mArchCover, iArchCover)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Armor) <> 0 Then
                    iArmor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Armor))
                    MainMenu.AddItem(iArmor)
                    MainMenu.BindMenuToItem(mArmor, iArmor)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Brakes) <> 0 Then
                    iBrakes = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Brakes))
                    MainMenu.AddItem(iBrakes)
                    MainMenu.BindMenuToItem(mBrakes, iBrakes)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Exhaust) <> 0 Then
                    iExhaust = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Exhaust))
                    MainMenu.AddItem(iExhaust)
                    MainMenu.BindMenuToItem(mExhaust, iExhaust)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Fender) <> 0 Then
                    iFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Fender))
                    MainMenu.AddItem(iFender)
                    MainMenu.BindMenuToItem(mFender, iFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Frame) <> 0 Then
                    iFrame = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Frame))
                    MainMenu.AddItem(iFrame)
                    MainMenu.BindMenuToItem(mFrame, iFrame)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Grille) <> 0 Then
                    iGrille = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Grille))
                    MainMenu.AddItem(iGrille)
                    MainMenu.BindMenuToItem(mGrille, iGrille)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hood) <> 0 Then
                    iHood = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hood))
                    MainMenu.AddItem(iHood)
                    MainMenu.BindMenuToItem(mHood, iHood)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Horns) <> 0 Then
                    iHorn = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Horns))
                    MainMenu.AddItem(iHorn)
                    MainMenu.BindMenuToItem(mHorn, iHorn)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Hydraulics) <> 0 Then
                    iHydraulics = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Hydraulics))
                    MainMenu.AddItem(iHydraulics)
                    MainMenu.BindMenuToItem(mHydraulics, iHydraulics)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Livery) <> 0 Then
                    iLivery = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Livery))
                    MainMenu.AddItem(iLivery)
                    MainMenu.BindMenuToItem(mLivery, iLivery)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Plaques) <> 0 Then
                    iPlaques = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Plaques))
                    MainMenu.AddItem(iPlaques)
                    MainMenu.BindMenuToItem(mPlaques, iPlaques)
                End If
                If Bennys.veh.GetModCount(VehicleMod.RightFender) <> 0 Then
                    iRFender = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.RightFender))
                    MainMenu.AddItem(iRFender)
                    MainMenu.BindMenuToItem(mRFender, iRFender)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Roof) <> 0 Then
                    iRoof = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Roof))
                    MainMenu.AddItem(iRoof)
                    MainMenu.BindMenuToItem(mRoof, iRoof)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Speakers) <> 0 Then
                    iSpeaker = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Speakers))
                    MainMenu.AddItem(iSpeaker)
                    MainMenu.BindMenuToItem(mSpeakers, iSpeaker)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Spoilers) <> 0 Then
                    iSpoilers = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Spoilers))
                    MainMenu.AddItem(iSpoilers)
                    MainMenu.BindMenuToItem(mSpoilers, iSpoilers)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Suspension) <> 0 Then
                    iSuspension = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Suspension))
                    MainMenu.AddItem(iSuspension)
                    MainMenu.BindMenuToItem(mSuspension, iSuspension)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Tank) <> 0 Then
                    iTank = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Tank))
                    MainMenu.AddItem(iTank)
                    MainMenu.BindMenuToItem(mTank, iTank)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Transmission) <> 0 Then
                    iTransmission = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Transmission))
                    MainMenu.AddItem(iTransmission)
                    MainMenu.BindMenuToItem(mTransmission, iTransmission)
                End If
                If Bennys.veh.GetModCount(VehicleMod.Trunk) <> 0 Then
                    iTrunk = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Trunk))
                    MainMenu.AddItem(iTrunk)
                    MainMenu.BindMenuToItem(mTrunk, iTrunk)
                End If
                iTurbo = New UIMenuItem(Helper.LocalizedModTypeName(VehicleToggleMod.Turbo))
                MainMenu.AddItem(iTurbo)
                MainMenu.BindMenuToItem(mTurbo, iTurbo)
                iTint = New UIMenuItem("Windows")
                MainMenu.AddItem(iTint)
                MainMenu.BindMenuToItem(mTint, iTint)
            End If

            MainMenu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuCloseHandler(sender As UIMenu)
        Try
            Game.FadeScreenOut(500)
            Wait(500)
            Bennys.isExiting = True
            camera.Stop()
            Bennys.veh.Position = New Vector3(-205.8678, -1321.805, 30.41191)
            Bennys.ply.Task.DriveTo(Bennys.veh, New Vector3(-205.743, -1303.657, 30.84998), 0.5, 5)
            Wait(500)
            Game.FadeScreenIn(500)
            Wait(10000)
            Bennys.ply.Task.ClearAll()
            Bennys.isExiting = False
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub MainMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            If sender Is MainMenu Then
                If selectedItem Is iRepair Then
                    Bennys.veh.Repair()
                    RefreshMenus()
                ElseIf selectedItem Is iUpgrade Then
                    Game.FadeScreenOut(500)
                    Wait(500)
                    Dim veh As Vehicle = World.CreateVehicle(Helper.LowriderUpgrade(Bennys.veh.Model), Bennys.veh.Position, Bennys.veh.Heading)
                    veh.IsPersistent = False
                    veh.PrimaryColor = Bennys.veh.PrimaryColor
                    veh.SecondaryColor = Bennys.veh.SecondaryColor
                    veh.DashboardColor = Bennys.veh.DashboardColor
                    veh.PearlescentColor = Bennys.veh.PearlescentColor
                    veh.TrimColor = Bennys.veh.TrimColor
                    veh.RimColor = Bennys.veh.RimColor
                    veh.NumberPlate = Bennys.veh.NumberPlate
                    Bennys.veh.Delete()
                    Bennys.ply.Task.WarpIntoVehicle(veh, VehicleSeat.Driver)
                    Bennys.veh = veh
                    veh.InstallModKit()
                    MainMenu.MenuItems.Remove(selectedItem)
                    RefreshMenus()
                    camera.RepositionFor(veh)
                    Wait(500)
                    Game.FadeScreenIn(500)
                    Helper.ScreenEffectStart(Helper.ScreenEffect.RaceTurbo, 1000)
                ElseIf selectedItem Is giEngine Then
                    Bennys.veh.OpenDoor(VehicleDoor.Hood, False, False)
                    camera.MainCameraPosition = CameraPosition.Engine
                ElseIf selectedItem Is giInterior Then
                    camera.MainCameraPosition = CameraPosition.Interior
                ElseIf selectedItem Is giWheels Then
                    camera.MainCameraPosition = CameraPosition.Wheels
                ElseIf selectedItem Is giLights Then
                    Bennys.veh.HighBeamsOn = True
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBodyworkMenu()
        Try
            gmBodywork = New UIMenu("", "BODYWORK")
            gmBodywork.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBodywork.MouseEdgeEnabled = False
            _menuPool.Add(gmBodywork)
            gmBodywork.AddItem(New UIMenuItem("Nothing"))
            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBodyworkMenu()
        Try
            gmBodywork.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.Aerials) <> 0 Then
                iAerials = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Aerials))
                gmBodywork.AddItem(iAerials)
                gmBodywork.BindMenuToItem(mAerials, iAerials)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Trim) <> 0 Then
                iTrim = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Trim))
                gmBodywork.AddItem(iTrim)
                gmBodywork.BindMenuToItem(mTrim, iTrim)
            End If
            gmBodywork.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateDoorMenu()
        Try
            gmDoor = New UIMenu("", "DOORS")
            gmDoor.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmDoor.MouseEdgeEnabled = False
            _menuPool.Add(gmDoor)
            gmDoor.AddItem(New UIMenuItem("Nothing"))
            gmDoor.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshDoorMenu()
        Try
            gmDoor.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.DoorSpeakers) <> 0 Then
                iDoor = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.DoorSpeakers))
                gmDoor.AddItem(iDoor)
                gmDoor.BindMenuToItem(mDoor, iDoor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Windows) <> 0 Then
                iWindows = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Windows))
                gmDoor.AddItem(iWindows)
                gmDoor.BindMenuToItem(mWindow, iWindows)
            End If
            gmDoor.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateEngineMenu()
        Try
            gmEngine = New UIMenu("", "ENGINE")
            gmEngine.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmEngine.MouseEdgeEnabled = False
            _menuPool.Add(gmEngine)
            gmEngine.AddItem(New UIMenuItem("Nothing"))
            gmEngine.RefreshIndex()
            AddHandler gmEngine.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEngineMenu()
        Try
            gmEngine.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.Engine) <> 0 Then
                iEngine = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Engine))
                gmEngine.AddItem(iEngine)
                gmEngine.BindMenuToItem(mEngine, iEngine)
            End If
            If Bennys.veh.GetModCount(VehicleMod.EngineBlock) <> 0 Then
                iEngineBlock = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.EngineBlock))
                gmEngine.AddItem(iEngineBlock)
                gmEngine.BindMenuToItem(mEngineBlock, iEngineBlock)
            End If
            If Bennys.veh.GetModCount(VehicleMod.AirFilter) <> 0 Then
                iAirFilter = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.AirFilter))
                gmEngine.AddItem(iAirFilter)
                gmEngine.BindMenuToItem(mAirFilter, iAirFilter)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Struts) <> 0 Then
                iStruts = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Struts))
                gmEngine.AddItem(iStruts)
                gmEngine.BindMenuToItem(mStruts, iStruts)
            End If
            gmEngine.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateInteriorMenu()
        Try
            gmInterior = New UIMenu("", "INTERIOR")
            gmInterior.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmInterior.MouseEdgeEnabled = False
            _menuPool.Add(gmInterior)
            gmInterior.AddItem(New UIMenuItem("Nothing"))
            gmInterior.RefreshIndex()
            AddHandler gmInterior.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshInteriorMenu()
        Try
            gmInterior.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.ColumnShifterLevers) <> 0 Then
                iColumnShifterLevers = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.ColumnShifterLevers))
                gmInterior.AddItem(iColumnShifterLevers)
                gmInterior.BindMenuToItem(mColumnShifterLevers, iColumnShifterLevers)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Dashboard) <> 0 Then
                iDashboard = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Dashboard))
                gmInterior.AddItem(iDashboard)
                gmInterior.BindMenuToItem(mDashboard, iDashboard)
                iDashboardColor = New UIMenuItem("Dashboard Color")
                gmInterior.AddItem(idashboardColor)
            End If
            If Bennys.veh.GetModCount(VehicleMod.DialDesign) <> 0 Then
                iDialDesign = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.DialDesign))
                gmInterior.AddItem(iDialDesign)
                gmInterior.BindMenuToItem(mDialDesign, iDialDesign)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Ornaments) <> 0 Then
                iOrnaments = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Ornaments))
                gmInterior.AddItem(iOrnaments)
                gmInterior.BindMenuToItem(mOrnaments, iOrnaments)
            End If
            If Bennys.veh.GetModCount(VehicleMod.Seats) <> 0 Then
                iSeats = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.Seats))
                gmInterior.AddItem(iSeats)
                gmInterior.BindMenuToItem(mSeats, iSeats)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SteeringWheels) <> 0 Then
                iSteeringWheels = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.SteeringWheels))
                gmInterior.AddItem(iSteeringWheels)
                gmInterior.BindMenuToItem(mSteeringWheels, iSteeringWheels)
            End If
            If Bennys.veh.GetModCount(VehicleMod.TrimDesign) <> 0 Then
                iTrimDesign = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.TrimDesign))
                gmInterior.AddItem(iTrimDesign)
                gmInterior.BindMenuToItem(mTrimDesign, iTrimDesign)
                iTrimColor = New UIMenuItem("Trim Color")
                gmInterior.AddItem(iTrimColor)
            End If
            gmInterior.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateBumperMenu()
        Try
            gmBumper = New UIMenu("", "BUMPER")
            gmBumper.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmBumper.MouseEdgeEnabled = False
            _menuPool.Add(gmBumper)
            gmBumper.AddItem(New UIMenuItem("Nothing"))
            gmBumper.RefreshIndex()
            AddHandler gmBumper.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshBumperMenu()
        Try
            gmBumper.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.FrontBumper) <> 0 Then
                giFBumper = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.FrontBumper))
                gmBumper.AddItem(giFBumper)
                gmBumper.BindMenuToItem(mFBumper, giFBumper)
            End If
            If Bennys.veh.GetModCount(VehicleMod.SideSkirt) <> 0 Then
                giSSkirt = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.SideSkirt))
                gmBumper.AddItem(giSSkirt)
                gmBumper.BindMenuToItem(mSSkirt, giSSkirt)
            End If
            If Bennys.veh.GetModCount(VehicleMod.RearBumper) <> 0 Then
                giRBumper = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.RearBumper))
                gmBumper.AddItem(giRBumper)
                gmBumper.BindMenuToItem(mRBumper, giRBumper)
            End If
            gmBumper.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelsMenu()
        Try
            gmWheels = New UIMenu("", "WHEELS")
            gmWheels.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWheels.MouseEdgeEnabled = False
            _menuPool.Add(gmWheels)
            gmWheels.AddItem(New UIMenuItem("Nothing"))
            gmWheels.RefreshIndex()
            AddHandler gmWheels.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler gmWheels.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelsMenu()
        Try
            gmWheels.MenuItems.Clear()
            giWheelType = New UIMenuItem("Wheel Type")
            gmWheels.AddItem(giWheelType)
            gmWheels.BindMenuToItem(gmWheelType, giWheelType)
            iRimColor = New UIMenuItem("Wheel Color")
            gmWheels.AddItem(iRimColor)
            giTires = New UIMenuItem("Tires")
            gmWheels.AddItem(giTires)
            gmWheels.BindMenuToItem(mTires, giTires)
            iTireSmoke = New UIMenuItem("Tire Smoke Color")
            gmWheels.AddItem(iTireSmoke)
            gmWheels.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateWheelTypeMenu()
        Try
            gmWheelType = New UIMenu("", "WHEEL TYPE")
            gmWheelType.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmWheelType.MouseEdgeEnabled = False
            _menuPool.Add(gmWheelType)
            gmWheelType.AddItem(New UIMenuItem("Nothing"))
            gmWheelType.RefreshIndex()
            AddHandler gmWheelType.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshWheelTypeMenu()
        Try
            gmWheelType.MenuItems.Clear()

            Select Case Bennys.veh.ClassType
                Case VehicleClass.Motorcycles, VehicleClass.Cycles
                    giBikeWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.BikeWheels))
                    gmWheelType.AddItem(giBikeWheels)
                    gmWheelType.BindMenuToItem(mBikeWheels, giBikeWheels)
                Case Else
                    giHighEndWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.HighEnd))
                    gmWheelType.AddItem(giHighEndWheels)
                    gmWheelType.BindMenuToItem(mHighEnd, giHighEndWheels)
                    giLowriderWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Lowrider))
                    gmWheelType.AddItem(giLowriderWheels)
                    gmWheelType.BindMenuToItem(mLowrider, giLowriderWheels)
                    giMuscleWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Muscle))
                    gmWheelType.AddItem(giMuscleWheels)
                    gmWheelType.BindMenuToItem(mMuscle, giMuscleWheels)
                    giOffroadWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Offroad))
                    gmWheelType.AddItem(giOffroadWheels)
                    gmWheelType.BindMenuToItem(mOffroad, giOffroadWheels)
                    giSportWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Sport))
                    gmWheelType.AddItem(giSportWheels)
                    gmWheelType.BindMenuToItem(mSport, giSportWheels)
                    giSUVWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.SUV))
                    gmWheelType.AddItem(giSUVWheels)
                    gmWheelType.BindMenuToItem(mSUV, giSUVWheels)
                    giTunerWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(VehicleWheelType.Tuner))
                    gmWheelType.AddItem(giTunerWheels)
                    gmWheelType.BindMenuToItem(mTuner, giTunerWheels)
                    giBennysWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(8)) 'Benny's Original
                    gmWheelType.AddItem(giBennysWheels)
                    gmWheelType.BindMenuToItem(mBennysOriginals, giBennysWheels)
                    giBespokeWheels = New UIMenuItem(Helper.GetLocalizedWheelTypeName(9)) 'Benny's Bespoke
                    gmWheelType.AddItem(giBespokeWheels)
                    gmWheelType.BindMenuToItem(mBespoke, giBespokeWheels)
            End Select

            gmWheelType.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateTyresMenu()
        Try
            mTires = New UIMenu("", "TIRES")
            mTires.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mTires.MouseEdgeEnabled = False
            _menuPool.Add(mTires)
            mTires.AddItem(New UIMenuItem("Nothing"))
            mTires.RefreshIndex()
            AddHandler mTires.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mTires.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshTyresMenu()
        Try
            mTires.MenuItems.Clear()

            Select Case Bennys.veh.WheelType
                Case 8, 9
                    iTires = New UIMenuItem(tyres(0))
                    With iTires
                        .SubInteger1 = 0
                        If Not Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
                Case Else
                    iTires = New UIMenuItem(tyres(0))
                    With iTires
                        .SubInteger1 = 0
                        If Not Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
                    iTires = New UIMenuItem(tyres(6))
                    With iTires
                        .SubInteger1 = 6
                        If Helper.IsCustomWheels Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    End With
                    mTires.AddItem(iTires)
            End Select

            mTires.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateMenu()
        Try
            gmPlate = New UIMenu("", "PLATE")
            gmPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmPlate.MouseEdgeEnabled = False
            _menuPool.Add(gmPlate)
            gmPlate.AddItem(New UIMenuItem("Nothing"))
            gmPlate.RefreshIndex()
            AddHandler gmPlate.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler gmPlate.OnItemSelect, AddressOf ModsMenuItemSelectHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPlateMenu()
        Try
            gmPlate.MenuItems.Clear()
            If Bennys.veh.GetModCount(VehicleMod.PlateHolder) <> 0 Then
                giPlateHolder = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.PlateHolder))
                gmPlate.AddItem(giPlateHolder)
                gmPlate.BindMenuToItem(mPlateHolder, giPlateHolder)
            End If
            If Bennys.veh.GetModCount(VehicleMod.VanityPlates) <> 0 Then
                giVanityPlate = New UIMenuItem(Helper.LocalizedModTypeName(VehicleMod.VanityPlates))
                gmPlate.AddItem(giVanityPlate)
                gmPlate.BindMenuToItem(mVanityPlates, giVanityPlate)
            End If
            giNumberPlate = New UIMenuItem("Number Plate")
            gmPlate.AddItem(giNumberPlate)
            gmPlate.BindMenuToItem(mNumberPlate, giNumberPlate)
            gmPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePlateNumberMenu()
        Try
            mNumberPlate = New UIMenu("", "NUMBER PLATE")
            mNumberPlate.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNumberPlate.MouseEdgeEnabled = False
            _menuPool.Add(mNumberPlate)
            mNumberPlate.AddItem(New UIMenuItem("Nothing"))
            mNumberPlate.RefreshIndex()
            AddHandler mNumberPlate.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mNumberPlate.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mNumberPlate.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPlateNumberMenu()
        Try
            mNumberPlate.MenuItems.Clear()

            Dim plateType As Array = System.Enum.GetValues(GetType(NumberPlateType))
            For Each plate As NumberPlateType In plateType
                iNumberPlate = New UIMenuItem([Enum].GetName(GetType(NumberPlateType), plate))
                With iNumberPlate
                    .SubInteger1 = plate
                    If Bennys.veh.NumberPlateType = plate Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                mNumberPlate.AddItem(iNumberPlate)
            Next
            mNumberPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshEnumModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef enumType As Helper.EnumTypes)
        Try
            menu.MenuItems.Clear()

            Dim enumArray As Array = Nothing
            Select Case enumType
                Case Helper.EnumTypes.NumberPlateType
                    enumArray = System.Enum.GetValues(GetType(NumberPlateType))
                    For Each enumItem As NumberPlateType In enumArray
                        item = New UIMenuItem(Helper.LocalizedLicensePlate(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.NumberPlateType = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleWindowTint
                    enumArray = System.Enum.GetValues(GetType(VehicleWindowTint))
                    For Each enumItem As VehicleWindowTint In enumArray
                        item = New UIMenuItem(Helper.LocalizedWindowsTint(enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.WindowTint = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorPrimary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PrimaryColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorSecondary
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.SecondaryColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.vehicleColorPearlescent
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.PearlescentColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorTrim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.TrimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorDashboard
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.DashboardColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorRim
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Bennys.veh.RimColor = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
                Case Helper.EnumTypes.VehicleColorAccent
                    enumArray = System.Enum.GetValues(GetType(VehicleColor))
                    For Each enumItem As VehicleColor In enumArray
                        item = New UIMenuItem([Enum].GetName(GetType(VehicleColor), enumItem))
                        With item
                            .SubInteger1 = enumItem
                            If Helper.GetAccentColor(Bennys.veh) = enumItem Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
                        End With
                        menu.AddItem(item)
                    Next
            End Select


            mNumberPlate.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateLightsMenu()
        Try
            gmLights = New UIMenu("", "LIGHTS")
            gmLights.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmLights.MouseEdgeEnabled = False
            _menuPool.Add(gmLights)
            gmLights.AddItem(New UIMenuItem("Nothing"))
            gmLights.RefreshIndex()
            AddHandler gmLights.OnMenuClose, AddressOf ModsMenuCloseHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshLightsMenu()
        Try
            gmLights.MenuItems.Clear()
            iHeadlights = New UIMenuItem("Headlights")
            gmLights.AddItem(iHeadlights)
            gmLights.BindMenuToItem(mHeadlights, iHeadlights)
            giNeonKits = New UIMenuItem("Neon Kits")
            gmLights.AddItem(giNeonKits)
            gmLights.BindMenuToItem(gmNeonKits, giNeonKits)
            gmLights.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonKitsMenu()
        Try
            gmNeonKits = New UIMenu("", "NEON KITS")
            gmNeonKits.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmNeonKits.MouseEdgeEnabled = False
            _menuPool.Add(gmNeonKits)
            gmNeonKits.AddItem(New UIMenuItem("Nothing"))
            gmNeonKits.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshNeonKitsMenu()
        Try
            gmNeonKits.MenuItems.Clear()
            iNeon = New UIMenuItem("Neon Layout")
            gmNeonKits.AddItem(iNeon)
            gmNeonKits.BindMenuToItem(mNeon, iNeon)
            iNeonColor = New UIMenuItem("Neon Color")
            gmNeonKits.AddItem(iNeonColor)
            gmNeonKits.BindMenuToItem(mNeonColor, iNeonColor)
            gmNeonKits.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateNeonMenu()
        Try
            mNeon = New UIMenu("", "NEON LAYOUT")
            mNeon.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            mNeon.MouseEdgeEnabled = False
            _menuPool.Add(mNeon)
            mNeon.AddItem(New UIMenuItem("Nothing"))
            mNeon.RefreshIndex()
            AddHandler mNeon.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler mNeon.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler mNeon.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshNeonMenu()
        Try
            mNeon.MenuItems.Clear()

            iNeon = New UIMenuItem("None")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.None
                If Helper.NeonLayout = Helper.NeonLayouts.None Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Front
                If Helper.NeonLayout = Helper.NeonLayouts.Front Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Back")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Back
                If Helper.NeonLayout = Helper.NeonLayouts.Back Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.Sides
                If Helper.NeonLayout = Helper.NeonLayouts.Sides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front and Back")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontAndBack
                If Helper.NeonLayout = Helper.NeonLayouts.FrontAndBack Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.FrontAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Back and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.BackAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.BackAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)
            iNeon = New UIMenuItem("Front, Back and Sides")
            With iNeon
                .SubInteger1 = Helper.NeonLayouts.FrontBackAndSides
                If Helper.NeonLayout = Helper.NeonLayouts.FrontBackAndSides Then .SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            mNeon.AddItem(iNeon)

            mNeon.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateResprayMenu()
        Try
            gmRespray = New UIMenu("", "RESPRAY")
            gmRespray.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            gmRespray.MouseEdgeEnabled = False
            _menuPool.Add(gmRespray)
            gmRespray.AddItem(New UIMenuItem("Nothing"))
            gmRespray.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshResprayMenu()
        Try
            gmRespray.MenuItems.Clear()
            giPrimaryCol = New UIMenuItem("Primary Color")
            gmRespray.AddItem(giPrimaryCol)
            giSecondaryCol = New UIMenuItem("Secondary Color")
            gmRespray.AddItem(giSecondaryCol)
            giAccentCol = New UIMenuItem("Accent Color")
            gmRespray.AddItem(giAccentCol)
            gmRespray.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreatePerformanceMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title.ToUpper)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshPerformanceMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod, ByRef gxt As String)
        Try
            menu.MenuItems.Clear()

            For i As Integer = -1 To Bennys.veh.GetModCount(vehmod) - 1
                Select Case vehmod
                    Case VehicleMod.Engine
                        item = New UIMenuItem(Game.GetGXTEntry(gxt & i + 2))
                    Case Else
                        item = New UIMenuItem(Game.GetGXTEntry(gxt & i + 1))
                End Select

                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub CreateModMenuFor(ByRef menu As UIMenu, ByRef title As String)
        Try
            menu = New UIMenu("", title.ToUpper)
            menu.SetBannerType(New Sprite("shopui_title_supermod", "shopui_title_supermod", Nothing, Nothing))
            menu.MouseEdgeEnabled = False
            _menuPool.Add(menu)
            menu.AddItem(New UIMenuItem("Nothing"))
            menu.RefreshIndex()
            AddHandler menu.OnMenuClose, AddressOf ModsMenuCloseHandler
            AddHandler menu.OnItemSelect, AddressOf ModsMenuItemSelectHandler
            AddHandler menu.OnIndexChange, AddressOf ModsMenuIndexChangedHandler
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleMod)
        Try
            menu.MenuItems.Clear()
            For i As Integer = -1 To Bennys.veh.GetModCount(vehmod) - 1
                item = New UIMenuItem(Helper.GetLocalizedModName(i, Bennys.veh.GetModCount(vehmod), vehmod))
                With item
                    If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                    .SubInteger1 = i
                    If Bennys.veh.GetMod(vehmod) = i Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                End With
                menu.AddItem(item)
            Next
            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub RefreshModMenuFor(ByRef menu As UIMenu, ByRef item As UIMenuItem, ByRef vehmod As VehicleToggleMod)
        Try
            menu.MenuItems.Clear()

            item = New UIMenuItem(Helper.LocalizedModTypeName(vehmod, True))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 0
                If Not Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)
            item = New UIMenuItem(Helper.LocalizedModTypeName(vehmod))
            With item
                If .Text = "NULL" Then .Text = Game.GetGXTEntry("CMOD_ARM_0")
                .SubInteger1 = 1
                If Bennys.veh.IsToggleModOn(vehmod) Then item.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            End With
            menu.AddItem(item)

            menu.RefreshIndex()
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuCloseHandler(sender As UIMenu)
        Try
            'Performance Mods
            Bennys.veh.SetMod(VehicleMod.Suspension, Bennys.lastVehMemory.Suspension, False)
            Bennys.veh.SetMod(VehicleMod.Armor, Bennys.lastVehMemory.Armor, False)
            Bennys.veh.SetMod(VehicleMod.Brakes, Bennys.lastVehMemory.Brakes, False)
            Bennys.veh.SetMod(VehicleMod.Transmission, Bennys.lastVehMemory.Transmission, False)
            Bennys.veh.SetMod(VehicleMod.Engine, Bennys.lastVehMemory.Engine, False)

            'Mods
            Bennys.veh.SetMod(VehicleMod.FrontBumper, Bennys.lastVehMemory.FrontBumper, False)
            Bennys.veh.SetMod(VehicleMod.RearBumper, Bennys.lastVehMemory.RearBumper, False)
            Bennys.veh.SetMod(VehicleMod.SideSkirt, Bennys.lastVehMemory.SideSkirt, False)
            Bennys.veh.NumberPlateType = Bennys.lastVehMemory.NumberPlate
            Bennys.veh.WheelType = Bennys.lastVehMemory.WheelType
            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.lastVehMemory.FrontWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.SetMod(VehicleMod.BackWheels, Bennys.lastVehMemory.BackWheels, Bennys.lastVehMemory.WheelsVariation)
            Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, Bennys.lastVehMemory.Headlights)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, Bennys.lastVehMemory.BackNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, Bennys.lastVehMemory.FrontNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, Bennys.lastVehMemory.LeftNeon)
            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, Bennys.lastVehMemory.RightNeon)
            Bennys.veh.SetMod(VehicleMod.ArchCover, Bennys.lastVehMemory.ArchCover, False)
            Bennys.veh.SetMod(VehicleMod.Exhaust, Bennys.lastVehMemory.Exhaust, False)
            Bennys.veh.SetMod(VehicleMod.Fender, Bennys.lastVehMemory.Fender, False)
            Bennys.veh.SetMod(VehicleMod.RightFender, Bennys.lastVehMemory.RightFender, False)
            Bennys.veh.SetMod(VehicleMod.DoorSpeakers, Bennys.lastVehMemory.DoorSpeakers, False)
            Bennys.veh.SetMod(VehicleMod.Frame, Bennys.lastVehMemory.Frame, False)
            Bennys.veh.SetMod(VehicleMod.Grille, Bennys.lastVehMemory.Grille, False)
            Bennys.veh.SetMod(VehicleMod.Hood, Bennys.lastVehMemory.Hood, False)
            Bennys.veh.SetMod(VehicleMod.Horns, Bennys.lastVehMemory.Horns, False)
            Bennys.veh.SetMod(VehicleMod.Hydraulics, Bennys.lastVehMemory.Hydraulics, False)
            Bennys.veh.SetMod(VehicleMod.Livery, Bennys.lastVehMemory.Livery, False)
            Bennys.veh.SetMod(VehicleMod.Plaques, Bennys.lastVehMemory.Plaques, False)
            Bennys.veh.SetMod(VehicleMod.Roof, Bennys.lastVehMemory.Roof, False)
            Bennys.veh.SetMod(VehicleMod.Speakers, Bennys.lastVehMemory.Speakers, False)
            Bennys.veh.SetMod(VehicleMod.Spoilers, Bennys.lastVehMemory.Spoilers, False)
            Bennys.veh.SetMod(VehicleMod.Aerials, Bennys.lastVehMemory.Aerials, False)
            Bennys.veh.SetMod(VehicleMod.Trim, Bennys.lastVehMemory.Trim, False)
            Bennys.veh.SetMod(VehicleMod.EngineBlock, Bennys.lastVehMemory.EngineBlock, False)
            Bennys.veh.SetMod(VehicleMod.AirFilter, Bennys.lastVehMemory.AirFilter, False)
            Bennys.veh.SetMod(VehicleMod.Struts, Bennys.lastVehMemory.Struts, False)
            Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, Bennys.lastVehMemory.ColumnShifterLevers, False)
            Bennys.veh.SetMod(VehicleMod.Dashboard, Bennys.lastVehMemory.Dashboard, False)
            Bennys.veh.SetMod(VehicleMod.DialDesign, Bennys.lastVehMemory.DialDesign, False)
            Bennys.veh.SetMod(VehicleMod.Ornaments, Bennys.lastVehMemory.Ornaments, False)
            Bennys.veh.SetMod(VehicleMod.Seats, Bennys.lastVehMemory.Seats, False)
            Bennys.veh.SetMod(VehicleMod.SteeringWheels, Bennys.lastVehMemory.SteeringWheels, False)
            Bennys.veh.SetMod(VehicleMod.TrimDesign, Bennys.lastVehMemory.TrimDesign, False)
            Bennys.veh.SetMod(VehicleMod.PlateHolder, Bennys.lastVehMemory.PlateHolder, False)
            Bennys.veh.SetMod(VehicleMod.VanityPlates, Bennys.lastVehMemory.VanityPlates, False)
            Bennys.veh.SetMod(VehicleMod.Tank, Bennys.lastVehMemory.Tank, False)
            Bennys.veh.SetMod(VehicleMod.Trunk, Bennys.lastVehMemory.Trunk, False)
            Bennys.veh.SetMod(VehicleMod.Windows, Bennys.lastVehMemory.Windows, False)
            Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, Bennys.lastVehMemory.Turbo)

            'Color
            Bennys.veh.DashboardColor = Bennys.lastVehMemory.DashboardColor
            Bennys.veh.TrimColor = Bennys.lastVehMemory.TrimColor

            'Close Doors
            If sender Is gmEngine Then Bennys.veh.CloseDoor(VehicleDoor.Hood, False)
            If sender Is gmLights Then Bennys.veh.HighBeamsOn = False

            'Reset Camera Position
            If (sender Is gmInterior) Or (sender Is gmEngine) Or (sender Is mFBumper) Or (sender Is mRBumper) Or (sender Is mSSkirt) Or (sender Is mNumberPlate) Or (sender Is mPlateHolder) Or
                (sender Is mVanityPlates) Or (sender Is gmWheels) Then
                camera.MainCameraPosition = CameraPosition.Car
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuItemSelectHandler(sender As UIMenu, selectedItem As UIMenuItem, index As Integer)
        Try
            For Each i As UIMenuItem In sender.MenuItems
                i.SetRightBadge(UIMenuItem.BadgeStyle.None)
            Next

            'Performance Mods
            If sender Is mSuspension Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Suspension, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Suspension = selectedItem.SubInteger1
                End If
            ElseIf sender Is mArmor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Armor, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Armor = selectedItem.SubInteger1
                End If
            ElseIf sender Is mBrakes Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Brakes, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Brakes = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTransmission Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Transmission, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Transmission = selectedItem.SubInteger1
                End If
            ElseIf sender Is mEngine Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Engine, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Engine = selectedItem.SubInteger1
                End If
            End If

            'Mods
            If sender Is mFBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.FrontBumper = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRBumper Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RearBumper, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.RearBumper = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSSkirt Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SideSkirt, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.SideSkirt = selectedItem.SubInteger1
                End If
            ElseIf sender Is mNumberPlate Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.NumberPlateType = selectedItem.SubInteger1
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.NumberPlate = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHeadlights Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Headlights = CBool(selectedItem.SubInteger1)
                End If
            ElseIf sender Is mArchCover Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ArchCover, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.ArchCover = selectedItem.SubInteger1
                End If
            ElseIf sender Is mExhaust Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Exhaust, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Exhaust = selectedItem.SubInteger1
                End If
            ElseIf sender Is mFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Fender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Fender = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRFender Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.RightFender, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.RightFender = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDoor Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DoorSpeakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.DoorSpeakers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mFrame Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Frame, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Frame = selectedItem.SubInteger1
                End If
            ElseIf sender Is mAerials Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Aerials, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Aerials = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrim Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trim, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Trim = selectedItem.SubInteger1
                End If
            ElseIf sender Is mEngineBlock Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.EngineBlock, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.EngineBlock = selectedItem.SubInteger1
                End If
            ElseIf sender Is mAirFilter Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.AirFilter, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.AirFilter = selectedItem.SubInteger1
                End If
            ElseIf sender Is mStruts Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Struts, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Struts = selectedItem.SubInteger1
                End If
            ElseIf sender Is mColumnShifterLevers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.ColumnShifterLevers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDashboard Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Dashboard, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Dashboard = selectedItem.SubInteger1
                End If
            ElseIf sender Is mDialDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.DialDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.DialDesign = selectedItem.SubInteger1
                End If
            ElseIf sender Is mOrnaments Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Ornaments, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Ornaments = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSeats Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Seats, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Seats = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSteeringWheels Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.SteeringWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.SteeringWheels = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrimDesign Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.TrimDesign, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.TrimDesign = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPlateHolder Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.PlateHolder, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.PlateHolder = selectedItem.SubInteger1
                End If
            ElseIf sender Is mVanityPlates Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.VanityPlates, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.VanityPlates = selectedItem.SubInteger1
                End If
            ElseIf sender Is mGrille Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Grille, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Grille = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHood Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hood, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Hood = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHorn Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Horns, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Horns = selectedItem.SubInteger1
                End If
            ElseIf sender Is mHydraulics Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Hydraulics, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Hydraulics = selectedItem.SubInteger1
                End If
            ElseIf sender Is mLivery Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Livery, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Livery = selectedItem.SubInteger1
                End If
            ElseIf sender Is mPlaques Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Plaques, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Plaques = selectedItem.SubInteger1
                End If
            ElseIf sender Is mRoof Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Roof, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Roof = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSpeakers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Speakers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Speakers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mSpoilers Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Spoilers, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Spoilers = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTank Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Tank, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Tank = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTrunk Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Trunk, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Trunk = selectedItem.SubInteger1
                End If
            ElseIf sender Is mWindow Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.Windows, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Windows = selectedItem.SubInteger1
                End If
            ElseIf sender Is mTurbo Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, CBool(selectedItem.SubInteger1))
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.Turbo = CBool(selectedItem.SubInteger1)
                End If
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case selectedItem.SubInteger1
                    Case Helper.NeonLayouts.None
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Front
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Back
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.Sides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.FrontAndBack
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = False
                            Bennys.lastVehMemory.RightNeon = False
                        End If
                    Case Helper.NeonLayouts.FrontAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = False
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.BackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = False
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                    Case Helper.NeonLayouts.FrontBackAndSides
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                            Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                            selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                            Bennys.lastVehMemory.FrontNeon = True
                            Bennys.lastVehMemory.BackNeon = True
                            Bennys.lastVehMemory.LeftNeon = True
                            Bennys.lastVehMemory.RightNeon = True
                        End If
                End Select
            End If

            'Wheels Mods
            If sender Is gmWheels Then
                If selectedItem Is giTires Then RefreshTyresMenu()
            End If
            If sender Is mBikeWheels Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    Bennys.veh.SetMod(VehicleMod.BackWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                    Bennys.lastVehMemory.BackWheels = selectedItem.SubInteger1
                End If
            ElseIf (sender Is mHighEnd) Or (sender Is mLowrider) Or (sender Is mMuscle) Or (sender Is mOffroad) Or (sender Is mSport) Or (sender Is mSUV) Or (sender Is mTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                    Bennys.veh.SetMod(VehicleMod.FrontWheels, selectedItem.SubInteger1, False)
                    selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                    Bennys.lastVehMemory.WheelType = Bennys.veh.WheelType
                    Bennys.lastVehMemory.FrontWheels = selectedItem.SubInteger1
                End If
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                    Case Else
                        If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
                            If selectedItem.SubInteger1 = 0 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = False
                            ElseIf selectedItem.SubInteger1 = 6 Then
                                Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), True)
                                If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), True)
                                selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
                                Bennys.lastVehMemory.WheelsVariation = True
                            End If
                        End If
                End Select
            End If

            'Wheel Type
            If sender Is gmWheelType Then
                If selectedItem Is giBikeWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.BikeWheels
                    RefreshModMenuFor(mBikeWheels, iBikeWheels, VehicleMod.BackWheels)
                ElseIf selectedItem Is giHighEndWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.HighEnd
                    RefreshModMenuFor(mHighEnd, iHighEnd, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giLowriderWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Lowrider
                    RefreshModMenuFor(mLowrider, iLowrider, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giMuscleWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Muscle
                    RefreshModMenuFor(mMuscle, iMuscle, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giOffroadWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Offroad
                    RefreshModMenuFor(mOffroad, iOffroad, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSportWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Sport
                    RefreshModMenuFor(mSport, iSport, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giSUVWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.SUV
                    RefreshModMenuFor(mSUV, iSUV, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giTunerWheels Then
                    Bennys.veh.WheelType = VehicleWheelType.Tuner
                    RefreshModMenuFor(mTuner, iTuner, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBennysWheels Then
                    Bennys.veh.WheelType = 8
                    RefreshModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
                ElseIf selectedItem Is giBespokeWheels Then
                    Bennys.veh.WheelType = 9
                    RefreshModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
                End If
            End If

            'Color
            'If sender Is mDashboardColor Then
            '    If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
            '        Bennys.veh.DashboardColor = selectedItem.SubInteger1
            '        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            '        Bennys.lastVehMemory.DashboardColor = selectedItem.SubInteger1
            '    End If
            'ElseIf sender Is mTrimColor Then
            '    If selectedItem.RightBadge = UIMenuItem.BadgeStyle.None Then
            '        Bennys.veh.TrimColor = selectedItem.SubInteger1
            '        selectedItem.SetRightBadge(UIMenuItem.BadgeStyle.Car)
            '        Bennys.lastVehMemory.TrimColor = selectedItem.SubInteger1
            '    End If
            'End If

            'Camera
            If sender Is gmBumper Then
                If selectedItem Is giFBumper Then
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                ElseIf selectedItem Is giRBumper Then
                    camera.MainCameraPosition = CameraPosition.RearBumper
                ElseIf selectedItem Is giSSkirt
                    camera.MainCameraPosition = CameraPosition.Car
                End If
            ElseIf sender Is gmPlate Then
                If selectedItem Is giNumberPlate Then
                    camera.MainCameraPosition = CameraPosition.BackPlate
                ElseIf selectedItem Is giPlateHolder Then
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                ElseIf selectedItem Is giVanityPlate Then
                    camera.MainCameraPosition = CameraPosition.FrontBumper
                End If
            End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Shared Sub ModsMenuIndexChangedHandler(sender As UIMenu, index As Integer)
        Try
            'Performance
            If sender Is mSuspension Then
                Bennys.veh.SetMod(VehicleMod.Suspension, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mArmor Then
                Bennys.veh.SetMod(VehicleMod.Armor, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mBrakes Then
                Bennys.veh.SetMod(VehicleMod.Brakes, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTransmission Then
                Bennys.veh.SetMod(VehicleMod.Transmission, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mEngine Then
                Bennys.veh.SetMod(VehicleMod.Engine, sender.MenuItems(index).SubInteger1, False)
            End If

            'Mod
            If sender Is mFBumper Then
                Bennys.veh.SetMod(VehicleMod.FrontBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRBumper Then
                Bennys.veh.SetMod(VehicleMod.RearBumper, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSSkirt Then
                Bennys.veh.SetMod(VehicleMod.SideSkirt, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mNumberPlate Then
                Bennys.veh.NumberPlateType = sender.MenuItems(index).SubInteger1
            ElseIf sender Is mHeadlights Then
                Bennys.veh.ToggleMod(VehicleToggleMod.XenonHeadlights, CBool(sender.MenuItems(index).SubInteger1))
            ElseIf sender Is mArchCover Then
                Bennys.veh.SetMod(VehicleMod.ArchCover, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mExhaust Then
                Bennys.veh.SetMod(VehicleMod.Exhaust, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFender Then
                Bennys.veh.SetMod(VehicleMod.Fender, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRFender Then
                Bennys.veh.SetMod(VehicleMod.RightFender, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDoor Then
                Bennys.veh.SetMod(VehicleMod.DoorSpeakers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mFrame Then
                Bennys.veh.SetMod(VehicleMod.Frame, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mAerials Then
                Bennys.veh.SetMod(VehicleMod.Aerials, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrim Then
                Bennys.veh.SetMod(VehicleMod.Trim, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mEngineBlock Then
                Bennys.veh.SetMod(VehicleMod.EngineBlock, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mAirFilter Then
                Bennys.veh.SetMod(VehicleMod.AirFilter, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mStruts Then
                Bennys.veh.SetMod(VehicleMod.Struts, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mColumnShifterLevers Then
                Bennys.veh.SetMod(VehicleMod.ColumnShifterLevers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDashboard Then
                Bennys.veh.SetMod(VehicleMod.Dashboard, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mDialDesign Then
                Bennys.veh.SetMod(VehicleMod.DialDesign, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mOrnaments Then
                Bennys.veh.SetMod(VehicleMod.Ornaments, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSeats Then
                Bennys.veh.SetMod(VehicleMod.Seats, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSteeringWheels Then
                Bennys.veh.SetMod(VehicleMod.SteeringWheels, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrimDesign Then
                Bennys.veh.SetMod(VehicleMod.TrimDesign, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mPlateHolder Then
                Bennys.veh.SetMod(VehicleMod.PlateHolder, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mVanityPlates Then
                Bennys.veh.SetMod(VehicleMod.VanityPlates, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mGrille Then
                Bennys.veh.SetMod(VehicleMod.Grille, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mHood Then
                Bennys.veh.SetMod(VehicleMod.Hood, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mHorn Then
                Bennys.veh.SetMod(VehicleMod.Horns, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mHydraulics Then
                Bennys.veh.SetMod(VehicleMod.Hydraulics, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mLivery Then
                Bennys.veh.SetMod(VehicleMod.Livery, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mPlaques Then
                Bennys.veh.SetMod(VehicleMod.Plaques, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mRoof Then
                Bennys.veh.SetMod(VehicleMod.Roof, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSpeakers Then
                Bennys.veh.SetMod(VehicleMod.Speakers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mSpoilers Then
                Bennys.veh.SetMod(VehicleMod.Spoilers, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTank Then
                Bennys.veh.SetMod(VehicleMod.Tank, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTrunk Then
                Bennys.veh.SetMod(VehicleMod.Trunk, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mWindow Then
                Bennys.veh.SetMod(VehicleMod.Windows, sender.MenuItems(index).SubInteger1, False)
            ElseIf sender Is mTurbo Then
                Bennys.veh.ToggleMod(VehicleToggleMod.Turbo, CBool(sender.MenuItems(index).SubInteger1))
            End If

            'Neons Mods
            If sender Is mNeon Then
                Select Case sender.MenuItems(index).SubInteger1
                    Case Helper.NeonLayouts.None
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Front
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Back
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.Sides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.FrontAndBack
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, False)
                    Case Helper.NeonLayouts.FrontAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.BackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, False)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                    Case Helper.NeonLayouts.FrontBackAndSides
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Back, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Front, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Left, True)
                        Bennys.veh.SetNeonLightsOn(VehicleNeonLight.Right, True)
                End Select
            End If

            'Wheels Mods
            If sender Is mBikeWheels Then
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
                Bennys.veh.SetMod(VehicleMod.BackWheels, sender.MenuItems(index).SubInteger1, False)
            ElseIf (sender Is mHighEnd) Or (sender Is mLowrider) Or (sender Is mMuscle) Or (sender Is mOffroad) Or (sender Is mSport) Or (sender Is mSUV) Or (sender Is mTuner) Or (sender Is mBennysOriginals) Or (sender Is mBespoke) Then
                Bennys.veh.SetMod(VehicleMod.FrontWheels, sender.MenuItems(index).SubInteger1, False)
            End If
            If sender Is mTires Then
                Select Case Bennys.veh.WheelType
                    Case 8, 9
                    Case Else
                        If sender.MenuItems(index).SubInteger1 = 0 Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), False)
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), False)
                        ElseIf sender.MenuItems(index).SubInteger1 = 6 Then
                            Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.FrontWheels), True)
                            If Bennys.veh.ClassType = VehicleClass.Motorcycles Then Bennys.veh.SetMod(VehicleMod.FrontWheels, Bennys.veh.GetMod(VehicleMod.BackWheels), True)
                        End If
                End Select
            End If

            'Color
            'If sender Is mDashboardColor Then
            '    Bennys.veh.DashboardColor = sender.MenuItems(index).SubInteger1
            'ElseIf sender Is mTrimColor Then
            '    Bennys.veh.TrimColor = sender.MenuItems(index).SubInteger1
            'End If
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub

    Public Sub New()
        _menuPool = New MenuPool()
        camera = New WorkshopCamera
        CreateMainMenu()
        CreateBodyworkMenu()
        CreateModMenuFor(mAerials, "AERIALS")
        CreateModMenuFor(mTrim, "TRIM")
        CreateEngineMenu()
        CreatePerformanceMenuFor(mEngine, "ENGINE")
        CreateModMenuFor(mEngineBlock, "ENGINE BLOCK")
        CreateModMenuFor(mAirFilter, "AIR FILTER")
        CreateModMenuFor(mStruts, "STRUTS")
        CreateInteriorMenu()
        CreateModMenuFor(mColumnShifterLevers, "COLUMN SHIFTER LEVERS")
        CreateModMenuFor(mDashboard, "DASHBOARD")
        CreateModMenuFor(mDialDesign, "DIAL DESIGN")
        CreateModMenuFor(mOrnaments, "ORNAMENTS")
        CreateModMenuFor(mSeats, "SEATS")
        CreateModMenuFor(mSteeringWheels, "STEERING WHEEL")
        CreateModMenuFor(mTrimDesign, "TRIM DESIGN")
        CreateDoorMenu()
        CreateModMenuFor(mDoor, "DOORS")
        CreateModMenuFor(mWindow, "WINDOW")
        CreateBumperMenu()
        CreateModMenuFor(mFBumper, "FRONT BUMPER")
        CreateModMenuFor(mRBumper, "REAR BUMPER")
        CreateModMenuFor(mSSkirt, "SIDE SKIRT")
        CreateWheelsMenu()
        CreateWheelTypeMenu()
        CreateModMenuFor(mBikeWheels, "BIKE WHEELS")
        CreateModMenuFor(mHighEnd, "HIGH END")
        CreateModMenuFor(mLowrider, "LOWRIDER")
        CreateModMenuFor(mMuscle, "MUSCLE")
        CreateModMenuFor(mOffroad, "OFFROAD")
        CreateModMenuFor(mSport, "SPORT")
        CreateModMenuFor(mSUV, "SUV")
        CreateModMenuFor(mTuner, "TUNER")
        CreateModMenuFor(mBennysOriginals, "BENNY'S ORIGINALS")
        CreateModMenuFor(mBespoke, "BENNY'S BESPOKE")
        CreateTyresMenu()
        CreatePlateMenu()
        CreateModMenuFor(mPlateHolder, "PLATE HOLDER")
        CreateModMenuFor(mVanityPlates, "VANITY PLATE")
        CreatePlateNumberMenu()
        CreateLightsMenu()
        CreateModMenuFor(mHeadlights, "HEADLIGHTS")
        CreateNeonKitsMenu()
        CreateNeonMenu()
        CreateResprayMenu()
        CreateModMenuFor(mArchCover, "ARCH COVER")
        CreateModMenuFor(mExhaust, "EXHAUST")
        CreateModMenuFor(mFender, "FENDER")
        CreateModMenuFor(mRFender, "RIGHT FENDER")
        CreateModMenuFor(mFrame, "ROLL CAGE")
        CreateModMenuFor(mGrille, "GRILLE")
        CreateModMenuFor(mHood, "HOOD")
        CreateModMenuFor(mHorn, "HORN")
        CreateModMenuFor(mHydraulics, "HYDRAULICS")
        CreateModMenuFor(mLivery, "LIVERY")
        CreateModMenuFor(mPlaques, "PLAQUES")
        CreateModMenuFor(mRoof, "ROOF")
        CreateModMenuFor(mSpeakers, "SPEAKERS")
        CreateModMenuFor(mSpoilers, "SPOILER")
        CreateModMenuFor(mTank, "TANK")
        CreateModMenuFor(mTrunk, "TRUNK")
        CreateModMenuFor(mTurbo, "TURBO")
        CreatePerformanceMenuFor(mSuspension, "SUSPENSION")
        CreatePerformanceMenuFor(mArmor, "ARMOR")
        CreatePerformanceMenuFor(mBrakes, "BRAKES")
        CreatePerformanceMenuFor(mTransmission, "TRANSMISSION")
    End Sub

    Public Shared Sub RefreshMenus()
        RefreshMainMenu()
        RefreshBodyworkMenu()
        RefreshModMenuFor(mAerials, iAerials, VehicleMod.Aerials)
        RefreshModMenuFor(mTrim, iTrim, VehicleMod.Trim)
        RefreshEngineMenu()
        RefreshPerformanceMenuFor(mEngine, iEngine, VehicleMod.Engine, "CMOD_ENG_")
        RefreshModMenuFor(mEngineBlock, iEngineBlock, VehicleMod.EngineBlock)
        RefreshModMenuFor(mAirFilter, iAirFilter, VehicleMod.AirFilter)
        RefreshModMenuFor(mStruts, iStruts, VehicleMod.Struts)
        RefreshInteriorMenu()
        RefreshModMenuFor(mColumnShifterLevers, iColumnShifterLevers, VehicleMod.ColumnShifterLevers)
        RefreshModMenuFor(mDashboard, iDashboard, VehicleMod.Dashboard)
        RefreshModMenuFor(mDialDesign, iDialDesign, VehicleMod.DialDesign)
        RefreshModMenuFor(mOrnaments, iOrnaments, VehicleMod.Ornaments)
        RefreshModMenuFor(mSeats, iSeats, VehicleMod.Seats)
        RefreshModMenuFor(mSteeringWheels, iSteeringWheels, VehicleMod.SteeringWheels)
        RefreshModMenuFor(mTrimDesign, iTrimDesign, VehicleMod.TrimDesign)
        RefreshDoorMenu()
        RefreshModMenuFor(mDoor, iDoor, VehicleMod.DoorSpeakers)
        RefreshModMenuFor(mWindow, iWindows, VehicleMod.Windows)
        RefreshBumperMenu()
        RefreshModMenuFor(mFBumper, iFBumper, VehicleMod.FrontBumper)
        RefreshModMenuFor(mRBumper, iRBumper, VehicleMod.RearBumper)
        RefreshModMenuFor(mSSkirt, iSideSkirt, VehicleMod.SideSkirt)
        RefreshWheelsMenu()
        RefreshWheelTypeMenu()
        RefreshModMenuFor(mBikeWheels, iBikeWheels, VehicleMod.BackWheels)
        RefreshModMenuFor(mHighEnd, iHighEnd, VehicleMod.FrontWheels)
        RefreshModMenuFor(mLowrider, iLowrider, VehicleMod.FrontWheels)
        RefreshModMenuFor(mMuscle, iMuscle, VehicleMod.FrontWheels)
        RefreshModMenuFor(mOffroad, iOffroad, VehicleMod.FrontWheels)
        RefreshModMenuFor(mSport, iSport, VehicleMod.FrontWheels)
        RefreshModMenuFor(mSUV, iSUV, VehicleMod.FrontWheels)
        RefreshModMenuFor(mTuner, iTuner, VehicleMod.FrontWheels)
        RefreshModMenuFor(mBennysOriginals, iBennys, VehicleMod.FrontWheels)
        RefreshModMenuFor(mBespoke, iBespoke, VehicleMod.FrontWheels)
        RefreshTyresMenu()
        RefreshPlateMenu()
        RefreshModMenuFor(mPlateHolder, iPlateHolder, VehicleMod.PlateHolder)
        RefreshModMenuFor(mVanityPlates, iVanityPlates, VehicleMod.VanityPlates)
        RefreshPlateNumberMenu()
        RefreshLightsMenu()
        RefreshModMenuFor(mHeadlights, iHeadlights, VehicleToggleMod.XenonHeadlights)
        RefreshNeonKitsMenu()
        RefreshNeonMenu()
        RefreshResprayMenu()
        RefreshModMenuFor(mArchCover, iArchCover, VehicleMod.ArchCover)
        RefreshModMenuFor(mExhaust, iExhaust, VehicleMod.Exhaust)
        RefreshModMenuFor(mFender, iFender, VehicleMod.Fender)
        RefreshModMenuFor(mRFender, iRFender, VehicleMod.RightFender)
        RefreshModMenuFor(mFrame, iFrame, VehicleMod.Frame)
        RefreshModMenuFor(mGrille, iGrille, VehicleMod.Grille)
        RefreshModMenuFor(mHood, iHood, VehicleMod.Hood)
        RefreshModMenuFor(mHorn, iHorn, VehicleMod.Horns)
        RefreshModMenuFor(mHydraulics, iHydraulics, VehicleMod.Hydraulics)
        RefreshModMenuFor(mLivery, iLivery, VehicleMod.Livery)
        RefreshModMenuFor(mPlaques, iPlaques, VehicleMod.Plaques)
        RefreshModMenuFor(mRoof, iRoof, VehicleMod.Roof)
        RefreshModMenuFor(mSpeakers, iSpeaker, VehicleMod.Speakers)
        RefreshModMenuFor(mSpoilers, iSpoilers, VehicleMod.Spoilers)
        RefreshModMenuFor(mTank, iTank, VehicleMod.Tank)
        RefreshModMenuFor(mTrunk, iTrunk, VehicleMod.Trunk)
        RefreshModMenuFor(mTurbo, iTurbo, VehicleToggleMod.Turbo)
        RefreshPerformanceMenuFor(mSuspension, iSuspension, VehicleMod.Suspension, "CMOD_SUS_")
        RefreshPerformanceMenuFor(mArmor, iArmor, VehicleMod.Armor, "CMOD_ARM_")
        RefreshPerformanceMenuFor(mBrakes, iBrakes, VehicleMod.Brakes, "CMOD_BRA_")
        RefreshPerformanceMenuFor(mTransmission, iTransmission, VehicleMod.Transmission, "CMOD_GBX_")
    End Sub

    Public Sub OnTick(sender As Object, e As EventArgs) Handles Me.Tick
        Try
            _menuPool.ProcessMenus()

            Select Case True
                Case MainMenu.Visible, gmBodywork.Visible
                    Game.DisableControlThisFrame(0, Control.VehicleAccelerate)
                    Game.DisableControlThisFrame(0, Control.VehicleAim)
                    Game.DisableControlThisFrame(0, Control.VehicleAttack)
                    Game.DisableControlThisFrame(0, Control.VehicleAttack2)
                    Game.DisableControlThisFrame(0, Control.VehicleBrake)
                    Game.DisableControlThisFrame(0, Control.VehicleCinCam)
                    Game.DisableControlThisFrame(0, Control.VehicleDuck)
                    Game.DisableControlThisFrame(0, Control.VehicleExit)
                    Game.DisableControlThisFrame(0, Control.VehicleHeadlight)
                    Game.DisableControlThisFrame(0, Control.VehicleHorn)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeftOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveRightOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveRight)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnLeftOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnRightOnly)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnHardLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleSubTurnHardRight)
                    Game.DisableControlThisFrame(0, Control.VehicleMoveLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleLookLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleLookRight)
                    Game.DisableControlThisFrame(0, Control.VehicleHotwireLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleHotwireRight)
                    Game.DisableControlThisFrame(0, Control.VehicleGunLeftRight)
                    Game.DisableControlThisFrame(0, Control.VehicleGunLeft)
                    Game.DisableControlThisFrame(0, Control.VehicleGunRight)
                    Game.DisableControlThisFrame(0, Control.VehicleCinematicLeftRight)
            End Select
        Catch ex As Exception
            Logger.Log(ex.Message & " " & ex.StackTrace)
        End Try
    End Sub
End Class
