// NAnt - A .NET build tool
// Copyright (C) 2001-2008 Gerry Shaw
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
// Gert Driesen (drieseng@users.sourceforge.net.be)

using System;
using System.Collections;
using System.Collections.Specialized;

namespace NAnt.VSNet {
    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationMap : IDictionary, ICollection, IEnumerable {
        #region Private Instance Fields

        private readonly Hashtable _innerHash;

        #endregion Private Instance Fields
        
        #region Public Instance Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationMap" /> class.
        /// </summary>
        public ConfigurationMap() {
            _innerHash = CollectionsUtil.CreateCaseInsensitiveHashtable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationMap" />
        /// class with the specified initial capacity. 
        /// </summary>
        /// <param name="capacity">The appropriate number of entries that the <see cref="ConfigurationMap" /> can initially contain.</param>
        public ConfigurationMap(int capacity) {
            _innerHash = CollectionsUtil.CreateCaseInsensitiveHashtable(capacity);
        }

        #endregion Public Instance Constructors

        #region Internal Instance Properties

        internal Hashtable InnerHash {
            get { return _innerHash; }
        }

        #endregion Internal Instance Properties

        #region Implementation of IDictionary

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns></returns>
        public ConfigurationMapEnumerator GetEnumerator() {
            return new ConfigurationMapEnumerator(this);
        }
        
        IDictionaryEnumerator IDictionary.GetEnumerator() {
            return GetEnumerator ();
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Removes the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Remove(Configuration configuration) {
            _innerHash.Remove(configuration);
        }

        void IDictionary.Remove(object key) {
            Remove((Configuration) key);
        }

        /// <summary>
        /// Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool Contains(Configuration key) {
            return _innerHash.Contains(key);
        }

        bool IDictionary.Contains(object key) {
            return Contains((Configuration) key);
        }

        /// <summary>
        /// Removes all elements from the <see cref="T:System.Collections.IDictionary" /> object.
        /// </summary>
        public void Clear() {
            _innerHash.Clear();      
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(Configuration key, Configuration value) {
            _innerHash.Add (key, value);
        }

        void IDictionary.Add(object key, object value) {
            Add((Configuration) key, (Configuration) value);
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> object is read-only.
        /// </summary>
        public bool IsReadOnly {
            get { return _innerHash.IsReadOnly; }
        }

        /// <summary>
        /// Gets or sets the <see cref="Configuration"/> with the specified key.
        /// </summary>
        /// <value>
        /// The <see cref="Configuration"/>.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public Configuration this[Configuration key] {
            get { return (Configuration) _innerHash[key]; }
            set { _innerHash[key] = value; }
        }

        object IDictionary.this[object key] {
            get { return this[(Configuration) key]; }
            set { this[(Configuration) key] = (Configuration) value; }
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
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        public int Count {
            get { return _innerHash.Count; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        public object SyncRoot {
            get { return _innerHash.SyncRoot; }
        }

        #endregion Implementation of ICollection
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationMapEnumerator : IDictionaryEnumerator {
        #region Private Instance Fields

        private readonly IDictionaryEnumerator _innerEnumerator;

        #endregion Private Instance Fields

        #region Internal Instance Constructors

        internal ConfigurationMapEnumerator(ConfigurationMap enumerable) {
            _innerEnumerator = enumerable.InnerHash.GetEnumerator();
        }

        #endregion Internal Instance Constructors

        #region Implementation of IDictionaryEnumerator

        /// <summary>
        /// Gets the key of the current dictionary entry.
        /// </summary>
        public Configuration Key {
            get { return (Configuration) _innerEnumerator.Key; }
        }

        object IDictionaryEnumerator.Key {
            get { return Key; }
        }

        /// <summary>
        /// Gets the value of the current dictionary entry.
        /// </summary>
        public Configuration Value {
            get { return (Configuration) _innerEnumerator.Value; }
        }

        object IDictionaryEnumerator.Value {
            get { return Value; }
        }

        /// <summary>
        /// Gets both the key and the value of the current dictionary entry.
        /// </summary>
        public DictionaryEntry Entry {
            get { return new DictionaryEntry (Key, Value); }
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
            get { return Current; }
        }

        /// <summary>
        /// Gets the current element in the collection.
        /// </summary>
        public ConfigurationMapEntry Current {
            get { return new ConfigurationMapEntry (Key, Value); }
        }

        #endregion Implementation of IEnumerator
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ConfigurationMapEntry {
        private readonly Configuration _key;
        private readonly Configuration _value;

        internal ConfigurationMapEntry(Configuration key, Configuration value) {
            _key = key;
            _value = value;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public Configuration Key {
            get { return _key; }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public Configuration Value {
            get { return _value; }
        }
    }
}
