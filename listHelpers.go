package main

import (
        "net/http"
        "fmt"
        "encoding/json"
        "database/sql"
)

//Encodes data from a query and sends it as a response
func queryEncodeSend(rows *sql.Rows, w http.ResponseWriter) {
        var encodingEntry listEntry
        var entries []listEntry
        var err error

        defer rows.Close()

        for rows.Next() {
                err = rows.Scan(&encodingEntry.Key, &encodingEntry.ConfLevel)

                if err != nil {
			fmt.Print(err)
                }
                entries = append(entries, encodingEntry)
        }

        w.Header().Set("Content-Type", "application/json")
        json.NewEncoder(w).Encode(entries)
}

//Decodes JSON request body into a struct
func decodeBodyEntryStruct(r *http.Request, decodingEntryPointer *listEntry) {
        decoder := json.NewDecoder(r.Body)
        err := decoder.Decode(decodingEntryPointer)
        if err != nil {
                fmt.Println(err)
        }
}

func getUUIDByEmail(email string, s *Server) string {
        var uuid string
        slct := "SELECT uuid FROM account_info WHERE email=$1"
        row := s.DB.QueryRow(slct, email)
        err := row.Scan(&uuid)

        if err != nil {
                fmt.Println(err)
        }

        return uuid
}

//Populates a single list entry
func populateListEntry(r *http.Request, s *Server, entryPtr *listEntry) {
        //Get the auth info
        email, _ := getAuthenticationInfo(r)

        //Get the uuid from the email row
        uuid := getUUIDByEmail(email, s)
        entryPtr.Email, entryPtr.UUID = email, uuid

        //Decode
        decodeBodyEntryStruct(r, entryPtr)
}

func errAndRespond(w http.ResponseWriter, err error, errorRes int, okRes int) {
        if err != nil {
                fmt.Println(err)
                w.WriteHeader(errorRes)
        } else {
                w.WriteHeader(okRes)
        }
}

