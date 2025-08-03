(ns elyses-destructured-enchantments)

(defn first-card
  "Returns the first card from deck."
  [[f]] f)

(defn second-card
  "Returns the second card from deck."
  [[_ s]] s)

(defn swap-top-two-cards
  "Returns the deck with first two items reversed."
  [[f s & remain]] (concat [s f] remain))

(defn discard-top-card
  "Returns a sequence containing the first card and
   a sequence of the remaining cards in the deck."
  [[f & remain]] [f remain])

(def face-cards
  ["jack" "queen" "king"])

(defn insert-face-cards
  "Returns the deck with face cards between its head and tail."
  [[f & remain]] (remove nil? (flatten [f face-cards remain])))
