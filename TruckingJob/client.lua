function spawnTruck()
    local vehicleName = 'Phantom'

    RequestModel(vehicleName)

    -- wait for the model to load
    while not HasModelLoaded(vehicleName) do
        Wait(500)
    end

    local vehicle = CreateVehicle(vehicleName, 129.94, 6424.31, 31.35, -148.13, true, false)

    SetEntityAsNoLongerNeeded(vehicle) -- lets game despawn vehicle
    SetModelAsNoLongerNeeded(vehicleName)

    TriggerEvent('chat:addMessage', {
        args = { 'Get in your ^*Truck!' }
    })
end

CreateThread(function()
    while true do
        Wait(0)
        DrawMarker(39, 123.19, 6406.92, 32.36, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0, 2.0, 245, 157, 121, 255, false, true, 2, false, nill, nill, false)
        DrawMarker(25, 123.19, 6406.92, 30.36, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 2.0, 2.0, 2.0, 245, 157, 121, 255, false, true, 2, false, nill, nill, false)
    end
end)

CreateThread(function()
    local threadID = GetIdOfThisThread()
    while true do
        Wait(5)
        local playerCoord = GetEntityCoords(PlayerPedId(), false)
        local locVector = vector3(123.19, 6406.92, 30.5)
        if Vdist2(playerCoord, locVector) < 2 then
            if(IsControlPressed(0, 38)) then
                spawnTruck()
                Wait(500)
                TerminateThread(threadID)
            end
        end
    end

end)

