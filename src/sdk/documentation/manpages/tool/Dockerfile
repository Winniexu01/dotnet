FROM wenwen01/prereqs:pandoc

ENTRYPOINT ["/usr/bin/env"]

RUN apk add git py3-pip && python3 -m pip install pandocfilters

CMD /manpages/tool/update-man-pages.sh