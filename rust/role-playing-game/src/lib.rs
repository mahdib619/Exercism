pub struct Player {
    pub health: u32,
    pub mana: Option<u32>,
    pub level: u32,
}

impl Player {
    pub fn new(level: u32) -> Player {
        Player {
            health: 100,
            mana: if level >= 10 { Some(100) } else { None },
            level,
        }
    }

    pub fn revive(&self) -> Option<Player> {
        if self.health > 0 {
            None
        } else {
            Some(Self::new(self.level))
        }
    }

    pub fn cast_spell(&mut self, mana_cost: u32) -> u32 {
        match self.mana {
            Some(m) => {
                if m >= mana_cost {
                    self.mana = Some(m - mana_cost);
                    mana_cost * 2
                } else {
                    0
                }
            }
            None => {
                self.health = if self.health > mana_cost { self.health - mana_cost } else { 0 };
                0
            }
        }
    }
}
