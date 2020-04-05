package main

import (
	"net/http"
	"github.com/gorilla/mux"
)

type listEntry struct {
	Email string `json: email`
	UUID string `json: uuid`
	Key string `json: key`
	ConfLevel int `json: confLevel`
}

//Handles adding a new list entry with a confidence level
//POST request with headers:
//Authorization: <email>:<hash>(base 64 encoded)
//To the url .../list
func (s *Server) handleAddListEntry() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {

		//Set up the list entry to insert
		var decodingEntry listEntry
		populateListEntry(r, s, &decodingEntry)

		//Insert into the lists table
		stmt := "INSERT INTO lists(email, uuid, analyzed_key, confidence_level) VALUES ($1, $2, $3, $4)"
		_, err := db.Exec(stmt, decodingEntry.Email, decodingEntry.UUID, decodingEntry.Key, decodingEntry.ConfLevel)

		//Check for error and respond appropriately
		errAndRespond(w, err, http.StatusBadRequest, http.StatusOK)
        }
}

//Handles updating 
//PATCH request with headers:
//Authorization: <email>:<hash>(base 64 encoded)
//To the url .../list
//Body must contain data of the form {key: <key for which the conf level will be updated>, confLevel: <new confidence level>}
func (s *Server) handleUpdateListEntry() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {
                //Set up the list entry for patching
		var decodingEntry listEntry
		populateListEntry(r, s, &decodingEntry)

		//Update the confidence level value of that key with the email and uuid
		stmt := "UPDATE lists SET confidence_level=$1 WHERE email=$2 AND uuid=$3 AND analyzed_key=$4"
		_, err := db.Exec(stmt, decodingEntry.ConfLevel, decodingEntry.Email, decodingEntry.UUID, decodingEntry.Key)

		//Check for error and respond appropriately
		errAndRespond(w, err, http.StatusBadRequest, http.StatusOK)
        }
}

//Handles removing a list entry based on the public key
//DELETE request with headers:
//Authorization: <email>:<hash>(base 64 encoded)
//To the url .../list
//Body must contain data of the form {key: <key to be deleted>}
func (s *Server) handleRemoveListEntry() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {
		var decodingEntry listEntry
		populateListEntry(r, s, &decodingEntry)

		//Delete from the lists table
		stmt := "DELETE FROM lists WHERE email=$1 AND uuid=$2 AND analyzed_key=$3"
		_, err := db.Exec(stmt, decodingEntry.Email, decodingEntry.UUID, decodingEntry.Key)

		//Check for error and respond appropriately
		errAndRespond(w, err, http.StatusBadRequest, http.StatusOK)
        }
}

//Handles fetching list entries based on email and uuid. Filtered in the url by confidence level
//GET request with headers:
//Authorization: <email>:<hash>(base 64 encoded)
//To th eurl .../list/?gt=<conf level to be greater than>&lt=<conf level to be less than>
func (s *Server) handleFetchListEntry() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {
		var decodingEntry listEntry
		populateListEntry(r, s, &decodingEntry)

		//Get from the lists table
		stmt := "SELECT analyzed_key, confidence_level FROM lists WHERE email=$1 AND uuid=$2 AND confidence_level>$3 AND confidence_level<$4"
		rows, err := db.Query(stmt, decodingEntry.Email, decodingEntry.UUID, mux.Vars(r)["gt"], mux.Vars(r)["lt"])

		//Check for error and respond appropriately
		errAndRespond(w, err, http.StatusBadRequest, http.StatusOK)

		queryEncodeSend(rows, w)
        }
}
