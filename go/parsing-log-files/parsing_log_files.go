package parsinglogfiles

import (
	"fmt"
	"regexp"
)

const userNameGroupName = "un"

var (
	isValidRegex        = regexp.MustCompile(`^\[((TRC)||(DBG)||(INF)||(WRN)||(ERR)||(FTL))\]`)
	lineSplitterRegex   = regexp.MustCompile(`<[~*=-]*>`)
	quotedPasswordRegex = regexp.MustCompile(`(?i)".*password.*"`)
	endOfLineRegex      = regexp.MustCompile(`end-of-line\d+`)
	usernameRegex       = regexp.MustCompile(fmt.Sprintf(`(User +)(?P<%s>[A-z0-9]+)`, userNameGroupName))
)

func IsValidLine(text string) bool {
	return isValidRegex.MatchString(text)
}

func SplitLogLine(text string) []string {
	return lineSplitterRegex.Split(text, -1)
}

func CountQuotedPasswords(lines []string) int {
	count := 0

	for _, line := range lines {
		if quotedPasswordRegex.MatchString(line) {
			count++
		}
	}

	return count
}

func RemoveEndOfLineText(text string) string {
	return endOfLineRegex.ReplaceAllString(text, "")
}

func TagWithUserName(lines []string) []string {
	taggedLines := make([]string, len(lines))

	for i := 0; i < len(lines); i++ {
		taggedLines[i] = tagWithUserName(lines[i])
	}

	return taggedLines
}

func tagWithUserName(line string) string {
	groups := getGroups(usernameRegex, line)

	if groups == nil {
		return line
	}

	return fmt.Sprintf("[USR] %s %s", groups[userNameGroupName][0], line)
}

func getGroups(regexp *regexp.Regexp, s string) map[string][]string {
	matches := regexp.FindStringSubmatch(s)

	if len(matches) == 0 {
		return nil
	}

	groups := make(map[string][]string)
	groupNames := usernameRegex.SubexpNames()

	for i := 0; i < len(groupNames); i++ {
		grpName := groupNames[i]

		if len(grpName) == 0{
			continue
		}

		match := matches[i]
		matchesGrp := groups[grpName]

		if matchesGrp == nil {
			matchesGrp = []string{}
		}

		if len(match) > 0 {
			matchesGrp = append(matchesGrp, match)
		}

		groups[grpName] = matchesGrp
	}

	return groups
}
