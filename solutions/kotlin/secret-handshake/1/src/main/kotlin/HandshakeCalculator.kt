object HandshakeCalculator {
    fun calculateHandshake(number: Int): List<Signal> {
        val signals = mutableListOf<Signal>()
        val bits = Integer.toBinaryString(number).reversed()

        for (i in bits.indices) {
            if (bits[i] == '1') {
                if (i < 4) {
                    signals.add(Signal.values()[i])
                } else {
                    signals.reverse()
                }
            }
        }

        return signals
    }
}
