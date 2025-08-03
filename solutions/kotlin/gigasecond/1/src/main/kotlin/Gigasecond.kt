import java.time.LocalDate
import java.time.LocalDateTime

class Gigasecond(val startDateTime: LocalDateTime) {

    constructor(startDate: LocalDate) : this(startDateTime = startDate.atStartOfDay())

    val date = startDateTime.plusSeconds(1000000000)
}
