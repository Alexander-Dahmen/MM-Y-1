```
megaman:
  movement:
    jumping
    walking
    sliding
    conveyors
    moving platforms
    physics sharing (with puppet gimmick)
    movement override? (idk how to describe this, what the whirlwind projectiles do)
  weapons:
    mega buster
    timber blade
    puppet shield
    angler bite
    mimic trap
    volt shock
    phoenix flight
    toxic bubble
    melon bomb
    rush coil
    rush jet
  animations:
    teleporting
    standing
    tiptoe
    walking
    sliding
    ladder climb
    ladder climb peak
    jumping
    damage taken
    shooting (stationary, walking, climbing, jumping)
    timber blade (stationary, walking, climbing, jumping)
    throwing (stationary, walking, climbing, jumping)
    phoenix dash
  other:
    health
enemies:
  general:
    base class for enemy handling
    enemy interactions
    death handling
    item drops
    miniboss handling
  enemy AIs:
    battonton ex
    beam crusher
    big shot g
    big shot s
    gabyoall
    metall ex
    obstructor
    r.r. sniper joe
    securobot
    shield attacker mb
    telly i
    wall slam
    bachisaw
    chainsoar c
    garywall
    puncho
    judii
    toybox spawner
    toybox spawnees
    lophii
    electrozoa
    garbotrout
    metall swim omega
    star fighter
    fujokuro
    metall ch
    elec'n mk ii
    joltie
    divechol
    tackle fire
    whirlwind slam
    beaker bomber
    toxair
    suikarora
    fruit drop
    skatoku
    axe max alpha
    shakkal bakkal
    magma processor
    canteloupe thrown
    honeydew thrown
  enemy projectiles:
    generic projectile
    beam crusher missiles
    big shot g shot
    obstructor charge shots
    wall slam walls
    chainsoar sawblade
    puncho cane
    metall swim omega bubbles
    whirlwind slam whirlwind
    toxair bombs
    fruit drop bombs
    [midboss projectiles insert here]
gimmicks:
  general:
    idek tbh
    interactions with player/enemies/other gimmicks
  gimmick list:
    conveyor belts
    switchable conveyor belt for timber fight
    logs
    voodoo dolls (stationary, regular, and inverse)
    button+door
    water
    darkness
    falling platforms
    arrow traps
    electric pipes
    switch blocks
    quick beams
    lava platforms
	special lava platform variant for phoenix fight (fuck phoenix man)
    chemicals (blue, red, yellow)
    breakable pipes (that release chemicals)
    chemical platforms (broken by yellow chemicals and toxair bombs)
    respawning chemical platforms for toxic fight
    crushers
    breakable blocks
    yoku blocks (5 pattern)
    switchable solids (for mimic woman and konatama cannon)
    teleporters (do these count as gimmicks? in the refights)
bosses:
  general:
    base class for boss handling
    boss rush handling
  boss AIs:
    optic devil
    timber man
    puppet man
    angler woman
    mimic woman
    volt man
    phoenix man
    toxic man
    melon man
    destroyer joe 2000
    interlude 2 boss
    great hydrozoa
    interlude
    konatama cannon
    heavy obstructor wall
    devil tokusentai
    dual thrown
    wily machine y+1
    wily capsule y+1
    super computer
other:
    menu handling
    save system
    nsf music player
    shop
    stage select
    fortress map system thingy
    customisable controls
    options
    cutscenes (probably overlaps with fortress map system but yeah)
    time trial mode if possible? (spawn player in level with no/all[but the RM's weapon if in a RM stage] weapons along with a timer, record fastest time and take back to menu upon defeating boss)
items:
    e tank
    w tank
    m tank
    guard unit
    eddie call
    shock guard
    energy balancer
    energy converter
    beat call
interactions:
    mimic trap --(pulls)-> projectiles
    mimic trap --(spawns)-> items
    wall slam wall --(pushes/carries)-> mega man
    puncho <- (pulls/manipulates AI)-> judii
    toybox spawner --(spawns)-> toybox spawnees
    whirlwind slam whirlwind --(moves)-> mega man
    toxair bombs --(destroy)-> chemical platforms
    conveyor belts --(pushes)-> mega man/items
    logs --(pushes/carries)-> mega man
    voodoo dolls --(affect)-> all enemies
    voodoo dolls --(share physics with)-> mega man
    button --(gets pushed by)-> mega man
    button --(opens)-> door
    water --(limits movement)-> electrozoa/garbotrout/lophii/metall swim omega
    water --(changes gravity)-> mega man/enemies/items
    darkness --(made less dark by)-> mega man/lophii/angler woman
    arrow traps --(detect and are triggered by)-> mega man
    electric pipes --(affect)-> water
    switch switches --(turn on/off)-> switch blocks
    chemical flows --(push)-> mega man/items
    timber man --(affects)-> switchable conveyor belt
    angler woman --(turns on/off)-> darkness
    mimic woman --(turns on/off)-> switchable solids
    switchable solids --(affects)-> arrow traps (affect the range of the arrow traps)
    volt man --(switches)-> switch blocks
    phoenix man --(activates)-> special lava platforms
    toxic man --(spawns/despawns)-> chemicals
    destroyer joe 2000 --(pushes)-> mega man
    great hydrozoa --(affects)-> water
    konatama cannon --(turns on/off)-> switchable solids

STUFF WHICH ISN'T QUITE 100% SOLIDIFIED YET:
    crushers
    breakable blocks
    melon man
    interlude 2 boss
    devil tokusentai
    dual thrown
    super computer
```