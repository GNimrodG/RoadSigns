# RoadSigns
KM/H HUD road signs in FiveM.
Download on the releases tab.

# Config
#### Note: If you edit the config you should know the basic JSON formattings.

- `"FadeEffects"` - Should the sign fade in or just appear? Values: `true` or `false`
- `"FlashEffect"` - Should every new sign flash when appear? Values: `true` or `false`
- `"DisplayCorner"` - In which corner should the sign be Values: `0`: Top-Left `1`: Top-Right `2`: Bottom-Left `3`: Bottom-Right
- `"SignGroups"` - It's the list containing the individual SignGroups.
  - `"Name"` - Each groups has it's own, must NOT match with any other group's name. Values: string between two `"` ex: `"Cars"`
  - `"UseText"` - Indicates if that sign group has "comment" on it. Values: `true` OR `false`
  - `"Text"` - If the groups has "comment" then what is that comment. Values: `null` if `"UseText"` is `false` othervise string between two `"` ex: `"TRUCKS"` *You should use capital lettrs for the look*
  - `"Signs"` - It's a list containing the object names and speed limits of the speed signs Values: `"object_id": "speed limit"`
