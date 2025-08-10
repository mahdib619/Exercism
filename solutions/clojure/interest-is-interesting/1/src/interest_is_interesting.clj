(ns interest-is-interesting)

(defn interest-rate
  "Returns the interest rate based on the specified balance."
  [balance]
  (condp #(< %2 %1) balance
    0 -3.213
    1000 0.5
    5000 1.621
    2.475))

(defn annual-balance-update
  "Returns the annual balance update, taking into account the interest rate."
  [balance]
  (+ (bigdec balance) (* (bigdec balance) (bigdec (/ (interest-rate balance) 100)))))

(defn amount-to-donate
  "Returns how much money to donate based on the balance and the tax-free percentage."
  [balance tax-free-percentage])