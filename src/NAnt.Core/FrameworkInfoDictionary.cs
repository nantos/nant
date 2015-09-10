// NAnt - A .NET build tool
// Copyright (C) 2002-2003 Scott Hernandez
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
//
// Ian MacLean (imaclean@gmail.com)

using System;
using System.Collections;

namespace NAnt.Core {
    /// <summary>
    /// Dictionary to collect the available frameworks.
    /// </summary>
    [Serializable()]
    public sealed class FrameworkInfoDictionary : IDictionary, ICollection, IEnumerable, ICloneable {
        #region Private Instance Fields

        private Hashtable _innerHash;

        #endregion Private Instance Fields
        
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary" /> class.
        /// </summary>
        public FrameworkInfoDictionary() {
            _innerHash = new Hashtable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="original">The original dictionary which well be copied.</param>
        public FrameworkInfoDictionary(FrameworkInfoDictionary original) {
            _innerHash = new Hashtable(original.InnerHash);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary dictionary which well be copied.</param>
        public FrameworkInfoDictionary(IDictionary dictionary) {
            _innerHash = new Hashtable (dictionary);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary" /> class
        /// with the specified capacity.
        /// </summary>
        public FrameworkInfoDictionary(int capacity) {
            _innerHash = new Hashtable(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="loadFactor">The load factor.</param>
        public FrameworkInfoDictionary(IDictionary dictionary, float loadFactor) {
            _innerHash = new Hashtable(dictionary, loadFactor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public FrameworkInfoDictionary(IEqualityComparer comparer) {
            _innerHash = new Hashtable(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="loadFactor">The load factor.</param>
        public FrameworkInfoDictionary(int capacity, int loadFactor) {
            _innerHash = new Hashtable(capacity, loadFactor);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="comparer">The comparer.</param>
        public FrameworkInfoDictionary(IDictionary dictionary, IEqualityComparer comparer) {
            _innerHash = new Hashtable (dictionary, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The comparer.</param>
        public FrameworkInfoDictionary(int capacity, IEqualityComparer comparer) {
            _innerHash = new Hashtable (capacity, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="loadFactor">The load factor.</param>
        /// <param name="comparer">The comparer.</param>
        public FrameworkInfoDictionary(IDictionary dictionary, float loadFactor, IEqualityComparer comparer) {
            _innerHash = new Hashtable (dictionary, loadFactor, comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkInfoDictionary"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="loadFactor">The load factor.</param>
        /// <param name="comparer">The comparer.</param>
        public FrameworkInfoDictionary(int capacity, float loadFactor, IEqualityComparer comparer) {
            _innerHash = new Hashtable (capacity, loadFactor, comparer);
        }

        #endregion Public Instance Constructors

        #region Internal Instance Properties

        internal Hashtable InnerHash {
            get { return _innerHash; }
            set { _innerHash = value ; }
        }

        #endregion Internal Instance Properties

        #region Implementation of IDictionary

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public FrameworkInfoDictionaryEnumerator GetEnumerator() {
            return new FrameworkInfoDictionaryEnumerator(this);
        }
        
        IDictionaryEnumerator IDictionary.GetEnumerator() {
            return new FrameworkInfoDictionaryEnumerator(this);
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the value with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public void Remove(string key) {
            _innerHash.Remove(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="T:System.Collections.IDictionary" />.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        void IDictionary.Remove(object key) {
            Remove((string) key);
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(string key) {
            return _innerHash.Contains(key);
        }

        bool IDictionary.Contains(object key) {
            return Contains((string)key);
        }

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public void Clear() {
            _innerHash.Clear();      
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
        public void Add(string key, FrameworkInfo value) {
            _innerHash.Add (key, value);
        }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
        void IDictionary.Add(object key, object value) {
            Add((string) key, (FrameworkInfo) value);
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return _innerHash.IsReadOnly; }
        }

        /// <summary>
        /// Gets or sets the <see cref="FrameworkInfo"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="FrameworkInfo"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public FrameworkInfo this[string key] {
            get { return (FrameworkInfo) _innerHash[key]; }
            set { _innerHash[key] = value; }
        }

        object IDictionary.this[object key] {
            get { return this[(string) key]; }
            set { this[(string) key] = (FrameworkInfo) value; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public ICollection Values {
            get { return _innerHash.Values; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys of the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public ICollection Keys {
            get { return _innerHash.Keys; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object has a fixed size.
        /// </summary>
        public bool IsFixedSize {
            get { return _innerHash.IsFixedSize; }
        }

        #endregion Implementation of IDictionary

        #region Implementation of ICollection

        void ICollection.CopyTo(Array array, int index) {
            _innerHash.CopyTo(array, index);
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized {
            get { return _innerHash.IsSynchronized; }
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the Dictionary.
        /// </summary>
        public int Count {
            get { return _innerHash.Count; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="ICollection"/>.
        /// </summary>
        public object SyncRoot {
            get { return _innerHash.SyncRoot; }
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        public void CopyTo(FrameworkInfo[] array, int index) {
            _innerHash.CopyTo(array, index);
        }

        #endregion Implementation of ICollection

        #region Implementation of ICloneable

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>The instance clone.</returns>
        public FrameworkInfoDictionary Clone() {
            FrameworkInfoDictionary clone = new FrameworkInfoDictionary();
            clone.InnerHash = (Hashtable) _innerHash.Clone();
            return clone;
        }

        object ICloneable.Clone() {
            return Clone();
        }

        #endregion Implementation of ICloneable

        #region HashTable Methods

        /// <summary>
        /// Determines whether the specified key contains key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKey (string key) {
            return _innerHash.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the specified value contains value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool ContainsValue(FrameworkInfo value) {
            return _innerHash.ContainsValue(value);
        }

        /// <summary>
        /// Synchronizeds the specified non synchronize.
        /// </summary>
        /// <param name="nonSync">The non synchronize.</param>
        /// <returns></returns>
        public static FrameworkInfoDictionary Synchronized(FrameworkInfoDictionary nonSync) {
            FrameworkInfoDictionary sync = new FrameworkInfoDictionary();
            sync.InnerHash = Hashtable.Synchronized(nonSync.InnerHash);
            return sync;
        }

        #endregion HashTable Methods
    }

    /// <summary>
    /// Enumerator for a <see cref="FrameworkInfoDictionary"/> instance.
    /// </summary>
    public class FrameworkInfoDictionaryEnumerator : IDictionaryEnumerator {
        #region Private Instance Fields

        private IDictionaryEnumerator _innerEnumerator;

        #endregion Private Instance Fields

        #region Internal Instance Constructors

        internal FrameworkInfoDictionaryEnumerator(FrameworkInfoDictionary enumerable) {
            _innerEnumerator = enumerable.InnerHash.GetEnumerator();
        }

        #endregion Internal Instance Constructors

        #region Implementation of IDictionaryEnumerator

        /// <summary>
        /// Gets the key of the current dictionary entry.
        /// </summary>
        public string Key {
            get { return (string) _innerEnumerator.Key; }
        }

        object IDictionaryEnumerator.Key {
            get { return Key; }
        }

        /// <summary>
        /// Gets the value of the current dictionary entry.
        /// </summary>
        public FrameworkInfo Value {
            get { return (FrameworkInfo) _innerEnumerator.Value; }
        }

        object IDictionaryEnumerator.Value {
            get { return Value; }
        }

        /// <summary>
        /// Gets both the key and the value of the current dictionary entry.
        /// </summary>
        public DictionaryEntry Entry {
            get { return _innerEnumerator.Entry; }
        }

        #endregion Implementation of IDictionaryEnumerator

        #region Implementation of IEnumerator

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset() {
            _innerEnumerator.Reset();
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext() {
            return _innerEnumerator.MoveNext();
        }

        object IEnumerator.Current {
            get { return _innerEnumerator.Current; }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public FrameworkInfo Current {
            get { return (FrameworkInfo)((DictionaryEntry)_innerEnumerator.Current).Value; }
        }

        #endregion Implementation of IEnumerator
    }
}
