package main

import (
	"fmt"
	"net/http"
	"strings"
        "encoding/base64"
	"encoding/json"
	"github.com/google/uuid"
)

//Represents an entry in the account_info table
type User struct {
	Email string
	UUID string 
	Hash string 
	PubKey string
}

//Extract the email and the hash and return them
func getAuthenticationInfo(r *http.Request) (string, string) {
        fmt.Print(r.Header.Get("Authorization"))
	decoded, err := base64.StdEncoding.DecodeString(r.Header.Get("Authorization"))

	if err != nil {
		fmt.Print(err)
	}

	authSlice := strings.Split(string(decoded), ":")

        //Return email and hash
        return authSlice[0], authSlice[1]
}
//Processes a POST request to the /signup endpont.
func (s *Server) handleSignUp() http.HandlerFunc {
        db := s.DB
        return func(w http.ResponseWriter, r *http.Request) {
		query := "INSERT INTO account_info(email, uuid, hash, pub_key) VALUES ($1, $2, $3, $4);"
		var u User

    		err := json.NewDecoder(r.Body).Decode(&u)
    		if err != nil {
        		http.Error(w, err.Error(), http.StatusBadRequest)
        		return
    		}else {
			u.UUID = uuid.New().String()			
			db.Exec(query, u.Email, u.UUID, u.Hash, u.PubKey)		
			w.WriteHeader(http.StatusOK)
			fmt.Println("correct")
		}
        }
}

func (s *Server) handleSignIn() http.HandlerFunc {
	//db := s.DB
	return func(w http.ResponseWriter, r *http.Request) {
		fmt.Println("Not implemented yet")
	}
}
